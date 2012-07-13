using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandi_s_Way
{
    public class Alarm
    {
        int? counter;

        public Alarm(int time)
        {
            counter = time;
        }
        public Alarm()
        {
            counter = null;
        }

        public bool IsDone()
        {
            if (counter != 0 && counter != null) counter--;

            if (counter == 0)
            {
                counter = null;
                return true;
            }
            else
                return false;
        } // when an alarm is done with its counting, it will return true, but after that it will return false untill its restarted
        public void Restart(int time)
        {
            counter = time;
        }
    }
}
