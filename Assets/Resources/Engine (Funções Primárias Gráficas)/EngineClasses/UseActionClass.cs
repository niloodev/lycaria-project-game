using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseActionClass
{
    public int habIndex;

    // retorna uma string de sucesso
    public string stringSuccess;


    public string[] targets_;

    public void setHabIndex(int ind)
    {
        habIndex = ind;
    }

    public void setParams(string string_, string[] targets)
    {
        stringSuccess = string_;
        targets_ = targets;
    }
   
}
