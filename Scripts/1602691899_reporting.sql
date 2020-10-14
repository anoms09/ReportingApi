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


IF (OBJECT_ID('get_transaction_count') is not null)
    BEGIN
        drop procedure get_transaction_count;
    END
GO

create procedure get_transaction_count
@startDate date,
@endDate date
as
select Count(1) from tbl_transactions 
where payment_date >= @startDate AND payment_date <= @endDate
GO


IF (OBJECT_ID('get_transaction_count_per_institution') is not null)
    BEGIN
        drop procedure get_transaction_count_per_institution;
    END
GO

create procedure get_transaction_count_per_institution
@startDate date,
@endDate date
as
select 
	Value  = Count(transaction_id) ,
	InstitutionName =  min (institution_name),
	InstitutionCode =  min (institution_code)

from tbl_transactions 
where payment_date >= @startDate AND payment_date <= @endDate
group by institutionCode
GO
