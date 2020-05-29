select * from divisionclassmapping
where strDivisionName = 'Leadline'


select p.strFirstName, p.strLastName, hr.intRiderNumber, hr.strHorseName, hr.intDivisionID, hrs.intOverallScore, hrs.intNum1stPlace, hrs.intNum2ndPlace,
                hrs.intNum3rdPlace, hrs.intNum4thPlace, hrs.intNum5thPlace  from HorseRider hr, HorseRiderScores hrs, Person p, ClassRegistration cr, ClassPlacing cp
                Where hr.intHorseRiderID = hrs.intHorseRiderID and hr.intPersonID = p.intPersonID and hr.intHorseRiderID = cr.intHorseRiderID 
				and hr.intHorseRiderID = cp.intHorseRiderID and cp.intClassID = cr.intClassID and  cr.intClassID = 14

select p.strFirstName, p.strLastName,hr.strHorseName,hr.intDivisionID,  cp.intHorseRiderID, cp.intClassID, cp.intPlace, hr.intRiderNumber from ClassPlacing cp , HorseRider hr, Person p where intClassID = 14 and cp.intHorseRiderID = hr.intHorseRiderID  and hr.intPersonID = p.intPersonID

select * from ClassPlacing


Select intRiderNumber from dbo.HorseRider where intRiderNumber is not null


select hr.intHorseRiderID, p.strFirstName, p.strLastName, hr.strHorseName, hr.intRiderNumber, hr.fltAmountDue, hr.ysnCheckedIn, hr.ysnHasPaid, d.strDivisionName from dbo.Person p,
    dbo.HorseRider hr, dbo.Division d where p.intPersonID = hr.intPersonID and hr.intDivisionID = d.intDivisionID


	select Count(1) from ClassRegistration where intHorseRiderID