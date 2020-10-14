IF (OBJECT_ID('usp_search_report') is not null)
    BEGIN
        drop procedure usp_search_report;
    END
GO

create procedure usp_search_report
@pageNumber int = 1,
@pageSize int =10,
@search varchar(50) = null,
@startDate date,
@endDate date
as
declare @from_row int = 1;

if @pageNumber > 1
begin
	set @from_row = ((@pageNumber * @pagesize) - (@pagesize)) + 1;
end;

with records as
(
select *, ROW_NUMBER()  over (order by payment_date desc ) as Row_Num 
from tbl_transactions (nolock) 

where 
	((@search is Null) 
	OR (narration like '%' + @search+ '%')
	OR (description like '%' + @search+ '%')
	OR (customerName like '%' + @search+ '%')
	OR (customerDescription like '%' + @search+ '%'))
	AND payment_date >= @startDate 
	AND payment_date  <= @endDate
),
rec_count as
(
	select count(*) totalcount from records
)
select * from records,rec_count where Row_Num between @from_row and (@from_row + @pagesize -1) order by payment_date desc
GO