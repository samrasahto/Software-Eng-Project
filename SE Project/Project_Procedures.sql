CREATE PROCEDURE USERID @User_Name nvarchar(30), @Password nvarchar(30)
AS
Select USER_NAME, Password_2 FROM [Login] Where User_Name = @User_Name AND Password_2 = @Password;
Go

CREATE PROCEDURE ViewProduct
AS
	SELECT Product_Name, f.Category_Name, Units_In_Stock, Discontinued, Unit_Price 
	FROM Products p, Food_Categories f 
	WHERE f.Category_ID = p.Food_Categories_Category_ID
	RETURN

CREATE PROCEDURE ViewEmployee

AS
	SELECT Employee_ID, First_Name, Last_Name, City_Name, Job_Name, Salary_Per_Hour, Department_Name, Contact, Gender, Email, [Address] 
	FROM Employee, City, Job, Departments 
	WHERE Employee.Departments_Department_ID=Departments.Department_ID AND Job.Job_ID=Employee.Job_ID AND Employee.City_idCity = City.idCity;
	RETURN


CREATE PROCEDURE ViewOrder
	
AS
	Select Order_ID, First_Name + ' '+ Last_Name as CustomerName,Order_Date, Shipping_Date, Expected_Date,
	Order_Status, Total from Customer,Orders where Customer.Customer_ID=Orders.Customer_ID;
	
	RETURN

CREATE PROCEDURE ViewCustomer
	
AS
	Select First_Name, Last_Name, Contact, [Address], Email, Gender, City_Name from Customer,City where
	Customer.City_idCity = City.idCity;
	RETURN