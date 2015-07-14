using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Model.Transfer
{
  public  class TimerModel
  {
      public TimerModel(int timeAmount, int sessionId, int studentId)
      {
          this.TimeIncrease = timeAmount;
          this.SessionId = sessionId;
          this.StudentId = studentId;
      }
      public int TimeIncrease;
      public int SessionId;
      public int StudentId;
  }
}
