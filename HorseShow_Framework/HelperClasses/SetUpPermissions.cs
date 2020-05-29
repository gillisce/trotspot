using HorseShow_Framework.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HorseShow_Framework.HelperClasses
{
    public class SetUpPermissions
    {
        readonly Security_DAL security_DAL = new Security_DAL();

        public PermissionSettings SetGlobalUserPermissions(string userName)
        {
            //  bool elevetionPriv = ElevateDevelopers(usr);
            return new PermissionSettings()
            {
                Showbill_Settings = security_DAL.CheckUserEligibility(userName, 1),
                CheckIn_Riders = security_DAL.CheckUserEligibility(userName, 2),
                View_ClassLineUp = security_DAL.CheckUserEligibility(userName, 3),
                PlaceClass = security_DAL.CheckUserEligibility(userName, 4),
                View_DivScores = security_DAL.CheckUserEligibility(userName, 5),
                View_ClassScores = security_DAL.CheckUserEligibility(userName, 6),
                BestInShow_Results = security_DAL.CheckUserEligibility(userName, 7),
                Admin = security_DAL.CheckUserEligibility(userName, 8),
                Developer = security_DAL.CheckUserEligibility(userName, 9),
              };
        }
    }
}