select * from class


select * from dbo.DivisionClassMapping where intDivisionID = 8

Delete from dbo.DivisionClassMapping where intDivisionID = 8

select * from HorseRider where ysnCheckedIn = 1


Update HorseRider
set ysnCheckedIn = 1 

select intClassID from class where intClassNumber = 1

select * from HorseRider



select * from HorseRiderScores

select * from ClassPlacing

select * from HorseRider 

select * from ClassRegistration


select Top 1 p.strFirstName, p.strLastName, hr.intRiderNumber, hr.strHorseName, hr.intDivisionID, hrs.intOverallScore, hrs.intNum1stPlace, hrs.intNum2ndPlace, hrs.intNum3rdPlace, hrs.intNum4thPlace, hrs.intNum5thPlace, strDivisionName  from HorseRider hr, HorseRiderScores hrs, Person p, Division d
Where hr.intHorseRiderID = hrs.intHorseRiderID and hr.intPersonID = p.intPersonID  and hr.intDivisionID = 1 and hr.intDivisionID = d.intDivisionID

select * from Person


