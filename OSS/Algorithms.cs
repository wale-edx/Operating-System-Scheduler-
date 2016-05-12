using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPUScheduler 
{
    class Algorithms
    {
        private static double waitingtime;
        
        public static double CalcWaitingTime(LinkedList<process> p)
        {
            process[] pp = new process[p.Count];
            p.CopyTo(pp, 0);
            double waiting = 0;
            double totalburst = 0.0;
            double[] wait = new double[p.Count];
            wait[0] = 0;
            for (int i = 1; i < p.Count; i++)
            {
                totalburst += pp[i - 1].getBurst();
                wait[i] = totalburst - pp[i].getArrival();
                waiting += wait [i];
            }
            return (waiting/p.Count);
        }
       
        public static LinkedList<process> FCFS(List<process> p , out double waiting)
        {       
            LinkedList<process> sorted_processes = new LinkedList<process>();
            List<process> SortedList = p.OrderBy(a => a.getArrival()).ToList();
            foreach (var item in SortedList) //copying arranged list in linked list
            {
                sorted_processes.AddLast(item);
            }
            waiting = CalcWaitingTime(sorted_processes);

          return sorted_processes ;
            
        }
        public static LinkedList<process> RoundRobin(double q, List<process> p, out double waiting)
        {
            double totalburst = 0.0;
            LinkedList<process> sorted_processes = new LinkedList<process>();
            List<process> SortedList = p.OrderBy(a => a.getArrival()).ToList();
            foreach (var item in SortedList) //copying arranged list in linked list
            {
                sorted_processes.AddLast(item);
                totalburst += item.getBurst();
            }
            double time = 0;
            time =SortedList.First().getArrival();
            double currentlyexecuted =0.0;
            int totaljobs = p.Count;
            double waitingtimeround = 0.0;
            int currentjob =0;
            LinkedList<process> final = new LinkedList<process>();
            while (currentlyexecuted < totalburst)
            {
                process current = sorted_processes.ElementAt(currentjob);
                
                if (current.getArrival() <= time)
                {

                    if ((current.getBurst() - current.getExe()) > 0)
                    {
                         
                        if ((current.getBurst() - current.getExe()) >= q)
                        {

                            //run if remaining time more than zero and q
                            current.setExe(current.getExe() + q);
                            final.AddLast(current);
                            waitingtimeround = waitingtimeround + (time - current.getDeparture());
                            time = time + q;
                            currentlyexecuted += q;
                            current.setDeparture(time);
                        }
                        else
                        {   //run with the remaining burst
                            current.setExe(current.getBurst());
                            final.AddLast(current);
                            waitingtimeround = waitingtimeround + (time - current.getDeparture());
                            time = time + current.getBurst();
                            currentlyexecuted += current.getBurst() ;
                            current.setDeparture(time);

                        }
                    }

                } currentjob = (++currentjob) % totaljobs;
                

            }
            waiting = (waitingtimeround / totaljobs);
            return final;

        }
        public static LinkedList<process> NonPreemptive_SJF(List<process> p, out double waiting)
        {
            LinkedList<process> sorted_processes = new LinkedList<process>();
            List<process> SortedBurst = p.OrderBy(b => b.getBurst()).ToList();

            double totalburst = 0.0;
            foreach (var item in SortedBurst) //copying arranged list in linked list
            {
                totalburst += item.getBurst();
            }
            int var = p.Count;
            

            for (double time = 0.0; time<=totalburst ; time++ )
            {
                for (int j = 0; j < var ; j++)
                {
                    if (SortedBurst[j].getArrival() <= time  )
                    {
                        sorted_processes.AddLast(SortedBurst[j]);
                        
                        time = time + SortedBurst[j].getBurst() - 1;
                        SortedBurst.RemoveAt(j);
                        var--;
                        break;
                        
                    }
                }
            }

         
           
            waiting = CalcWaitingTime(sorted_processes);
            return sorted_processes;
        }

        public static LinkedList<process> NonPreemptive_Priority(List<process> p, out double waiting)
        {
            LinkedList<process> sorted_processes = new LinkedList<process>();
            List<process> SortedPriority = p.OrderBy(pr => pr.getPriority()).ToList();

            double totalburst = 0.0;
            foreach (var item in SortedPriority) //copying arranged list in linked list
            {
                totalburst += item.getBurst();
            }
            int var = p.Count;


            for (double time = 0.0; time <= totalburst; time++)
            {
                for (int j = 0; j < var; j++)
                {
                    if (SortedPriority[j].getArrival() <= time)
                    {
                        sorted_processes.AddLast(SortedPriority[j]);

                        time = time + SortedPriority[j].getBurst() - 1;
                        SortedPriority.RemoveAt(j);
                        var--;

                        break;
                    }
                }
            }



             waiting = CalcWaitingTime(sorted_processes);
            return sorted_processes;
        }
        public static LinkedList<process> Preemptive_SJF(List<process> p, out double waiting)
        {
            LinkedList<process> sorted_processes = new LinkedList<process>();
            List<process> SortedBurst = p.OrderBy(b => (b.getBurst()-b.getExe())).ToList();

            double totalburst = 0.0;
            foreach (var item in SortedBurst) //copying arranged list in linked list
            {
                totalburst += item.getBurst();
            }
            int var = p.Count;
            double waitingtime = 0.0;
            process old = SortedBurst[0];
            foreach (var item in SortedBurst)
            {
                if (item.getArrival() < old.getArrival())
                { old = item; }
            }
            
            for (double time = 0.0; time <= totalburst; time++)
            {
                
                SortedBurst = SortedBurst.OrderBy(b => (b.getBurst() - b.getExe())).ToList();
                for (int j = 0; j < var; j++)
                {
                   
                    if (SortedBurst[j].getArrival() <= time)
                    {
                        if (old != SortedBurst[j])
                        {
                            waitingtime = waitingtime + (time - SortedBurst[j].getDeparture());
                            old.setDeparture(time);
                            //SortedBurst[j].setDeparture(time);
                            old = SortedBurst[j];
                        }
                        
                        sorted_processes.AddLast(SortedBurst[j]);
                        SortedBurst[j].setExe((SortedBurst[j].getExe())+1);
                        //SortedBurst[j].setDeparture = time
                        if(SortedBurst[j].getBurst()==SortedBurst[j].getExe()){
                            SortedBurst.RemoveAt(j);
                            var--;
                        }
                        
                        
                        break;

                    }
                }
            }

            LinkedList<process> final = new LinkedList<process>();
            process dublicated = sorted_processes.ElementAt(0);
            final.AddLast(dublicated);
            foreach (process x in sorted_processes)
            {
                if (x != final.Last())
                {
                    final.AddLast(x);
                }
            }

            //return waitingtime = CalcWaitingTime(sorted_processes);
            waiting = waitingtime/p.Count;
            return final;
        }
        public static LinkedList<process> Preemptive_Priority(List<process> p, out double waiting)
        {
            LinkedList<process> sorted_processes = new LinkedList<process>();
            List<process> SortedPriority = p.OrderBy(pr => pr.getPriority()).ToList();
            List<process> Sortedarrival = p.OrderBy(pr => pr.getArrival()).ToList();
            //List<process> SortedBurst = p.OrderBy(b => (b.getBurst() - b.getExe())).ToList();

            double totalburst = 0.0;
            foreach (var item in SortedPriority) //copying arranged list in linked list
            {
                totalburst += item.getBurst();
            }
            int var = p.Count;
            double waitingtime = 0.0;
            process old = SortedPriority[0];
            foreach (var item in SortedPriority)
            {
                if (item.getArrival() < old.getArrival())
                { old = item; }
            }

            for (double time = (Sortedarrival.First().getArrival()); time <= totalburst; time++)
            {

                //SortedBurst = SortedBurst.OrderBy(b => (b.getBurst() - b.getExe())).ToList();
                for (int j = 0; j < var; j++)
                {

                    if (SortedPriority[j].getArrival() <= time)
                    {
                        if (old != SortedPriority[j])
                        {
                            waitingtime = waitingtime + (time - SortedPriority[j].getDeparture());
                            old.setDeparture(time);
                            //SortedBurst[j].setDeparture(time);
                            old = SortedPriority[j];
                        }

                        sorted_processes.AddLast(SortedPriority[j]);
                        SortedPriority[j].setExe((SortedPriority[j].getExe()) + 1);
                        //SortedBurst[j].setDeparture = time
                        if (SortedPriority[j].getBurst() == SortedPriority[j].getExe())
                        {
                            SortedPriority.RemoveAt(j);
                            var--;
                        }


                        break;

                    }
                }
            }
            LinkedList<process> final = new LinkedList<process>();
            process dublicated = sorted_processes.ElementAt(0);
            final.AddLast(dublicated);
            foreach (process x in sorted_processes)
            {
                if(x != final.Last())
                {
                    final.AddLast(x);
                }
            }
            //return waitingtime = CalcWaitingTime(sorted_processes);
            waiting =waitingtime / p.Count;
            return final;

        }   

    };
}
