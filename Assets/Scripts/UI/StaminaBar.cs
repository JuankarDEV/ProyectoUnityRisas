using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public Slider staminaSlider;    // Slider UI que representa la barra de stamina

    public float maxStamina = 100;    // Stamina máxima que puede tener el jugador

    private float currentStamina;   // Stamina actual

    private float regenerateStaminaTime = 0.1f; // Tiempo entre regeneraciones de stamina

    private float regenerateAmount = 2; // Cantidad de stamina que se regenera en cada paso

    private float losingStaminaTime = 0.1f;  // Tiempo entre pérdida de stamina


     private Coroutine myCoroutineLosing; // Referencia a la corrutina que pierde stamina
    private Coroutine myCoroutineRegenerate;  // Referencia a la corrutina que regenera stamina



        // Start se ejecuta una vez al comenzar el juego o al activar el objeto
    void Start()
    {

        currentStamina = maxStamina;   // Inicializar stamina al máximo

        staminaSlider.maxValue = maxStamina; // Configurar valor máximo del slider
        staminaSlider.value = maxStamina; // Inicializar slider en máximo





    }
// Método público para usar stamina (por ejemplo, al correr)
    public void UseStamina(float amount)
    {  // Verificar que haya suficiente stamina para gastar
        if (currentStamina - amount > 0)
        {  // Si ya hay una corrutina consumiendo stamina, detenerla para reiniciarla
            if (myCoroutineLosing != null)
            {
                StopCoroutine(myCoroutineLosing);
            }
        // Iniciar la corrutina para perder stamina progresivamente
             myCoroutineLosing = StartCoroutine(LosingStaminaCoroutine(amount));
               // Si hay una corrutina regenerando stamina, detenerla para que no interfiera
            if (myCoroutineRegenerate != null)
            {
                StopCoroutine(myCoroutineRegenerate);
            }
            // Iniciar corrutina para regenerar stamina después
            myCoroutineRegenerate = StartCoroutine(RegenerateStaminaCoroutine());
        }
        else
        {  // No hay stamina suficiente: mostrar mensaje y detener sprint
            Debug.Log("No hay Stamina");
            FindObjectOfType<Control>().isSprinting = false;
        }
    }
 // Corrutina que reduce stamina poco a poco
    private IEnumerator LosingStaminaCoroutine(float amount)
    {
        while (currentStamina > 0)
        {
            currentStamina -= amount;    // Reducir stamina

            staminaSlider.value = currentStamina;     // Actualizar barra

            yield return new WaitForSeconds(losingStaminaTime); // Esperar antes de siguiente reducción

        }

        myCoroutineLosing = null; // Limpia referencia al acabar

 // Al quedarse sin stamina, desactivar sprint del jugador
        FindObjectOfType<Control>().isSprinting = false;

    }
     // Corrutina que regenera stamina poco a poco
        private IEnumerator RegenerateStaminaCoroutine()
    {
        yield return new WaitForSeconds(1); // Esperar 1 segundo antes de empezar a regenerar

        while (currentStamina < maxStamina)
        {
            currentStamina += regenerateAmount;  // Sumar stamina

            staminaSlider.value = currentStamina;  // Actualizar barra

            yield return new WaitForSeconds(regenerateStaminaTime);  // Esperar antes de siguiente regeneración
        }

        myCoroutineRegenerate = null; // Limpiar referencia cuando termina

    }
}
