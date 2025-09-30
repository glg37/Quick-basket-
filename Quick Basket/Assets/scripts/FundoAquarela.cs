using UnityEngine;

public class FundoAquarela : MonoBehaviour
{
    public float velocidade = 0.5f; 
    private Camera cam;

    
    private Color[] cores = new Color[]
    {
        new Color32(99, 41, 41, 255),  
        new Color32(40, 82, 45, 255),   
        new Color32(65, 94, 134, 255),  
        new Color32(79, 57, 91, 255),   
        new Color32(124, 122, 62, 255), 
        new Color32(147, 59, 114, 255), 
        new Color32(50, 120, 110, 255)  
    };

    private int indiceAtual = 0;
    private int indiceProximo = 1;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.backgroundColor = cores[indiceAtual];
    }

    void Update()
    {
       
        cam.backgroundColor = Color.Lerp(cam.backgroundColor, cores[indiceProximo], Time.deltaTime * velocidade);

        
        if (Vector4.Distance(cam.backgroundColor, cores[indiceProximo]) < 0.01f)
        {
            indiceAtual = indiceProximo;
            indiceProximo = (indiceProximo + 1) % cores.Length; 
        }
    }
}
