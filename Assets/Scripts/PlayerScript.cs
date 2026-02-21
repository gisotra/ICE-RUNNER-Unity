using UnityEngine;
using DG.Tweening; 

public class PlayerScript : MonoBehaviour
{
    
    /*Instance Tools*/
    public Rigidbody2D myBody;
    private GameControls gc;
    public LogicScript ls;

    /*Jump Related*/
    private float jumpStrength = 15.5f;
    private bool on_floor = false;


    
    /*Movement Related*/
    private float horizontal_speed = 5.5f;

    /*Death*/
    private Vector3 initialPos;
    private bool dead = false;
    private Vector3 OriginalScale; 


    private void Awake()
    {
        // Encontro as instâncias dos Controles (input) e estabeleço a comunicação com a Lógica Principal do jogo 
        gc = new GameControls();
        ls = GameObject.FindGameObjectWithTag("Main Logic").GetComponent<LogicScript>();
        initialPos = transform.position; 
        OriginalScale = transform.localScale; //transform é uma abreviação para pegar o componente Transform do meu GameObject
    }


    private void OnEnable() //chamado toda vez que o objeto é ativado
    {
        gc.Enable();
        gc.Gameplay.Jump.performed += ctx => Jump();
        gc.Gameplay.Jump.canceled += ctx => CutJump();
        /*
         
        ctx é uma instância da struct CallbackContext da biblioteca de Inputs do Unity,
        ela é responsável por lidar com os eventos de callback (função passada de argumento para outra
        função) das entradas de input do jogo. 
        Nesse código, ' => ' é um lambda que descreve que, caso o EVENTO jump ocorra, eu chamo o método Jump, da classe
        do player. 
         */
    }

    private void OnDisable() //chamado toda vez que o objeto é desativado
    {
        gc.Disable();
        gc.Gameplay.Jump.performed -= ctx => Jump();
        gc.Gameplay.Jump.canceled += ctx => CutJump();
        //o operador -= NESSE CONTEXTO equivale a "remova essa operação dos inputs"
    }


    void FixedUpdate()
    {
        if (dead)
        {
            return;
        }
        Vector2 moveVec = gc.Gameplay.Move.ReadValue<Vector2>();
        myBody.linearVelocity = new Vector2(moveVec.x * horizontal_speed, myBody.linearVelocity.y); //apenas altero a velocidade horizontal

        /*if (myBody.linearVelocityY == 0) //estou no chão
        {
            on_floor = true;
        }*/
    }

    private void Jump()
    {
        if (dead) return;

        if (on_floor)
        {
            /*
            entendendo um pouco mais da biblioteca DOTween, ela aplica efeitos de animação em qualquer valor que eu desejar
            e aplicar efeitos de durabilidade, intensidade e força da animação (Ex: bounce, Ease In, Ease out, Spring...)
            */
            transform.DOKill(); //com isso aqui eu mato as animações de squash anteriores 
            //eu achato meu player

            //eu estico meu player
            transform.localScale = new Vector3(OriginalScale.x * 0.5f, OriginalScale.y * 1.3f, OriginalScale.z); 
            transform.DOScale(OriginalScale, 0.4f).SetEase(Ease.InBack);

            myBody.linearVelocity = Vector2.up * jumpStrength;
            on_floor = false;
        }
    }

    private void CutJump()
    {
        if (myBody.linearVelocity.y > 0) { //pulo dinamico com base no tempo que está apertando o botão de pulo
            myBody.linearVelocity *= new Vector2(myBody.linearVelocity.x, 0.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Ground>() != null)
        {
            if(!on_floor) //acabei de "pousar"
            { 
                // eu"amasso" o player, pra dar a impressão de impacto
                transform.DOKill();            
                transform.localScale = new Vector3(OriginalScale.x * 1.3f, OriginalScale.y * 0.7f, OriginalScale.z); 
                transform.DOScale(OriginalScale, 0.2f).SetEase(Ease.InBack);
            }       
            on_floor = true;     
            return;
        }
        //não sei se essa é a abordagem mais usada por profissionais, mas utilizei para verificar se eu estou colidindo com um obstáculo, não com o chão
        if(collision.collider.GetComponent<Obstacles>() != null)
        {
            dead = true;
            ls.game_is_running = false;
            ls.GameOver();
            return;
        }

    }

    public void RestartPosition()
    {
        dead = false;
        transform.position = initialPos;
    }
}
