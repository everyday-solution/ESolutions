using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESolutions.Core
{
    /// <summary>
    /// A single holiday
    /// </summary>
    public class Holiday
    {
        //Properties
        #region Day
        /// <summary>
        /// Gets the day.
        /// </summary>
        /// <value>
        /// The day.
        /// </value>
        public DateTime Day
        {
            get;
            private set;
        }
        #endregion

        #region GermanHoliday
        /// <summary>
        /// Gets the german holiday.
        /// </summary>
        /// <value>
        /// The german holiday.
        /// </value>
        public GermanHolidays GermanHoliday
        {
            get;
            private set;
        }
        #endregion

        #region Name
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public String Name
        {
            get;
            private set;
        }
        #endregion

        //Constructors
        #region Holiday
        internal Holiday(DateTime day, GermanHolidays holiday, String name)
        {
            this.Day = day;
            this.GermanHoliday = holiday;
            this.Name = name;
        }
        #endregion
    }
}
