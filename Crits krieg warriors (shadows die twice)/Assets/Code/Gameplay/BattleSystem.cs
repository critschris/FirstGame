using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON,LOST};

public class BattleSystem : MonoBehaviour
{

    public BattleState state;
    public int turn=0;

    public GameObject player;
    public GameObject enemy;

    public Transform playerBattleSation;
    public Transform enemyBattleSation;

    public BackGroundChanger backGroundChange;
    public int bg_to_use;

    public Text DialogText;
    public Text TurnText;

    Unit playerUnit;
    Unit enemyUnit;

    public UnitHUD playerHUD;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {

        FindObjectOfType<AudioManager>().Play("BGM");
        backGroundChange.bgChange(bg_to_use);
        GameObject playerGO = Instantiate(player, playerBattleSation);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemy, enemyBattleSation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        DialogText.text = "A " + enemyUnit.name + " Approaches!";
        TurnText.text = "Turn" + turn;

        playerHUD.SetupHUD(playerUnit);

        TurnText.text = "Turn 1";

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
       
        PlayerTurn();
    }

    void PlayerTurn()
    {
        turn = turn + 1;
        TurnText.text = "Turn " +turn;
        DialogText.text = "Please pick an action";

    }

    IEnumerator EnemyTurn()
    {
        turn = turn + 1;
        TurnText.text = "Turn " + turn;
        DialogText.text = enemyUnit.name+" attacks you!";

        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.takeDamage(enemyUnit.atk);
        playerHUD.HPupdate(playerUnit);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

 

    IEnumerator PlayerAttack(Unit enemy)
    {
        //yield return new WaitForSeconds(1f);

        DialogText.text = "You attack the enemy";

        FindObjectOfType<AudioManager>().Play("Cut");

        bool isDead = enemy.takeDamage(playerUnit.atk);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            
        StartCoroutine(EnemyTurn());
            
        }

    }


    IEnumerator PlayerWait(Unit enemy)
    {
        yield return new WaitForSeconds(1f);

        DialogText.text = "You wait (pussy)";

        FindObjectOfType<AudioManager>().Play("Wait");

        yield return new WaitForSeconds(2f);

        StartCoroutine(EnemyTurn());


    }

    public void OnAttackButton()
    {

        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(PlayerAttack(enemyUnit));
        }
        else
        {
            return;
        }
    }

    public void OnWaitButton()
    {
        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(PlayerWait(enemyUnit));
        }
        else
        {
            return;
        }
    }
    
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            DialogText.text = "Winner Baby Let's goooo!";
        }
        else if (state == BattleState.LOST)
        {
            DialogText.text = "Loser Boo Hoo :(";
        }
    }


    public void setbg(int a)
    {
        this.bg_to_use = a;
    }
    
}
