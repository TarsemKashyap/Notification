IF COL_LENGTH('Merchant_Configuration','Verification_Method') IS  NULL
BEGIN
ALTER TABLE Merchant_Configuration ADD Verification_Method INT NOT NULL DEFAULT(1)
end
GO