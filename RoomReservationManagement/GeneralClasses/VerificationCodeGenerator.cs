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
     * 
     * New ways of mangling the old code can be implemented if so desired
     */


    public class VerificationCodeGenerator
    {
        public ApplicationDbContext databaseConnection = new ApplicationDbContext();
     
        
        //the default value should only happen one time, when no other code exists in the system
		/// <summary>
		/// gets the next possible verification code, this function
		/// will ensure we do not reuse codes and that it is sudo random
		/// 
		/// </summary>
		/// <returns></returns>
        public int getVerificationCode()
        {
            int newCode;
            int lastUsedCode = databaseConnection.Res_Reservations.Select(p => p.ver_code).DefaultIfEmpty(1000).Max();

            //since the default value is 0 we need to make sure we dont get a 0 value back
            if(lastUsedCode == 0)
            {
                lastUsedCode = 1000;
            }
            int ranValue = getRandomNumber(15, 175);
            newCode = lastUsedCode + ranValue;
            return newCode;
        }


		/// <summary>
		/// Random number generator
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
        public int getRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }




    }


}