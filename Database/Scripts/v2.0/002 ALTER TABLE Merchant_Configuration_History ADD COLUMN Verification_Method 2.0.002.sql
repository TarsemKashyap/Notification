IF COL_LENGTH('Merchant_Configuration_History','Verification_Method') IS  NULL
BEGIN
ALTER TABLE Merchant_Configuration_History ADD Verification_Method INT NOT NULL DEFAULT(1)
end
GO