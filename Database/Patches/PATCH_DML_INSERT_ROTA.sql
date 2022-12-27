
USE [yamaharota]
GO

SET XACT_ABORT ON
GO

BEGIN TRANSACTION
GO

INSERT INTO Rota (Origem,Destino,Valor) VALUES ( 'GUA' ,'BRC' ,'10');
INSERT INTO Rota (Origem,Destino,Valor) VALUES ( 'BRC' ,'SCL' ,'5');
INSERT INTO Rota (Origem,Destino,Valor) VALUES ( 'GUA' ,'CDG' ,'75');
INSERT INTO Rota (Origem,Destino,Valor) VALUES ( 'GUA' ,'SCL' ,'20');
INSERT INTO Rota (Origem,Destino,Valor) VALUES ( 'GUA' ,'ORL' ,'56');
INSERT INTO Rota (Origem,Destino,Valor) VALUES ( 'ORL' ,'CDG' ,'5');
INSERT INTO Rota (Origem,Destino,Valor) VALUES ( 'SCL' ,'ORL' ,'20');
 
GO

COMMIT TRANSACTION
GO