insert into Users(Guid, Name, Surname, DesiredWeightInKgs, HeightInMeters) values 
('35dacbf7-c939-4112-aa86-bee0792e37d3', 'Diego', 'Ceccacci', 87, 1.87)

declare @userId as int = @@identity


insert into WeightHistory(DateOfMeasurement, WeightInKgs, UserId) values
('2021-04-25', 97.35, @userId),
('2021-04-26', 96.65, @userId),
('2021-04-27', 97.10, @userId),
('2021-04-28', 96.65, @userId),
('2021-04-29', 96.50, @userId),
('2021-05-01', 96.70, @userId),
('2021-05-03', 96.55, @userId),
('2021-05-05', 96.35, @userId),
('2021-05-08', 96.35, @userId),
('2021-05-11', 96.10, @userId),
('2021-05-12', 96.35, @userId),
('2021-05-15', 95.15, @userId),
('2021-05-17', 95.75, @userId),
('2021-05-19', 96.45, @userId),
('2021-05-22', 96.05, @userId),
('2021-05-24', 95.90, @userId),
('2021-05-26', 95.90, @userId),
('2021-06-02', 96.25, @userId)


--delete from Users; delete from WeightHistory;
--select * from Users; select * from WeightHistory;