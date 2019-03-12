using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RoomReservationManagement.Models;

namespace RoomReservationManagement.GeneralClasses
{

    /*
     * This class is used to create the verification code that 
     * will be sent along with catering orders for a reservation.
     * This way we can confirm that the order being placed is in fact for a reservation 
     * and not someone placing a fake order.
     * The codes will start at 1000 and work upwards, it is not sequential and will be 
     * pseudo random, but will work off the last greatest entry in the reservation table.
     * Example: last code used is "1431" we will pull it, add a random value to it and 
     * insert that as the new value.
     */


    public class VerificationCodeGenerator
    {
        public ApplicationDbContext databaseConnection = new ApplicationDbContext();
     
        
        //the default value should only happen one time, when no other code exists in the system
        public int getVerificationCode()
        {
            int newCode;
            int lastUsedCode = databaseConnection.Res_Reservations.Select(p => p.ver_code).DefaultIfEmpty(1000).Max();
            int ranValue = getRandomNumber(15, 175);
            newCode = lastUsedCode + ranValue;
            return newCode;
        }



        public int getRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }




    }


}