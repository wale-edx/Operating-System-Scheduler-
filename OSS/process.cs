using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class process
{   
    private string name;
    private int priority;
    private double burst, arrivaltime, exe, departure;

    public void setBurst(double b)
    {
        burst = b;
    }
    public void setExe(double e)
    {
        exe = e;
    }
    public void setPriority(int p)
    {
        priority = p;
    }
    public void setArrival(double a)
    {
        arrivaltime = a;
    }
    public void setName(string n)
    {
        name = n;
    }
    
    public double getBurst()
    {
        return burst;
    }
    public double getExe()
    {
        return exe;
    }
    public int getPriority()
    {
        return priority;
    }
    public double getArrival()
    {
        return arrivaltime;
    }
    public string getName()
    {
        return name;
    }
    public double getDeparture()
    {
        return departure;
    }
    public void setDeparture(double d)
    {
         departure =d;
    }
    public process(string n, double a, double b)
    {
        name = n;
        burst = b;
        arrivaltime = a;
        priority = 0;
        departure = arrivaltime;
        exe = 0;
    }
    public process(string n, double a, double b, int p)
    {
        name = n;
        burst = b;
        arrivaltime = a;
        priority = p;
        departure = a;
        exe = 0;
    }

};