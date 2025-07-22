create database pos;

use pos;

CREATE TABLE Product (
    id INT IDENTITY(1,1) primary key,
    name VARCHAR(100) NOT NULL,
    category VARCHAR(50) NOT NULL,
    unit_price DECIMAL(10,2) NOT NULL,
    purchase_price DECIMAL(10,2) NOT NULL,
    quantity INT NOT NULL,
    
);

CREATE TABLE Invoices (
    id INT IDENTITY(1,1) PRIMARY KEY,
    created_at DATETIME DEFAULT GETDATE(),
    total DECIMAL(10,2) NOT NULL,
    user_id INT NOT NULL
);

CREATE TABLE Users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(10) NOT NULL
);

CREATE TABLE Sales (
    id INT IDENTITY(1,1) PRIMARY KEY,
    invoice_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity_sold INT NOT NULL,
    total_price DECIMAL(10,2) NOT NULL
);

CREATE TABLE Inventory (
    id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT NOT NULL FOREIGN KEY REFERENCES Product(id),
    reorder_level INT DEFAULT 2
);

--First SignIn
--username = admin1
--password = admin0123
INSERT INTO Users (username, password, role, IsDeleted)
VALUES ('admin1', 'a529dfcdd72cd20f1dd17160976bc3b5db6073c538a1c6f1a7439daa1b63ce76', 'admin', 0);


use pos;
select * from Users;

UPDATE Inventory SET reorder_level = 2 WHERE reorder_level IS NULL;

INSERT INTO Inventory (product_id, reorder_level)
SELECT id, 2 FROM Product
WHERE id NOT IN (SELECT product_id FROM Inventory);

INSERT INTO Inventory (product_id, reorder_level)
SELECT id, 2 FROM Product
WHERE id NOT IN (SELECT product_id FROM Inventory);

ALTER TABLE Product 
ADD IsDeleted BIT NOT NULL DEFAULT 0;

ALTER TABLE Users 
ADD IsDeleted BIT NOT NULL DEFAULT 0;

use pos;
select * from Returns;
ALTER TABLE Sales
ADD sale_date DATETIME NOT NULL DEFAULT GETDATE();

CREATE TABLE Returns (
    id INT IDENTITY(1,1) PRIMARY KEY,
    invoice_id INT NOT NULL FOREIGN KEY REFERENCES Invoices(id),
    product_id INT NOT NULL FOREIGN KEY REFERENCES Product(id),
    quantity INT NOT NULL,
    returned_amount DECIMAL(10,2) NOT NULL,
    profit_deduction DECIMAL(10,2) NOT NULL,
    return_date DATETIME DEFAULT GETDATE()
);

