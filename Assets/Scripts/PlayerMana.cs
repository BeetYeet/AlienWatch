using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Mana")]
    [Space]

    public int maxMana = 10;
    public int mana = 10;
	public int ticksPerMana = 10;
	public int ticksLeft = 10;




	// Start is called before the first frame update
	void Start()
    {
        GameController.curr.Tick += Tick;
    }

    void Tick()
    {
		ticksLeft--;
		if ( ticksLeft == 0 )
		{
			ticksLeft = ticksPerMana;
			if (mana < maxMana)
			{
				mana++;
			}
		  if(mana == maxMana)
			{
				mana = maxMana;
			}
		}
    }

}
