using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManagerScript : MonoBehaviour
{
    public AssetReference audioReference; // eu faço uma referencia ao audio 
    private AudioSource audioSource;// capturo o componente que toca o áudio

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        audioReference.LoadAssetAsync<AudioClip>().Completed += OnAudioLoaded;
        
    }

    private void OnAudioLoaded(AsyncOperationHandle<AudioClip> handle)
    {
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            audioSource.clip = handle.Result;
            audioSource.Play();
        } else
        {
            Debug.LogError("Falha ao carregar a música");
        }

    }

    /*Explicação do código:
    Aqui utilizei a extensão dos Addressables, que é uma forma muito mais eficiente de carregar assets durante a criação 
    do jogo a partir do assincronismo da thread principal. Ele faz isso a partir do método LoadAssetAsync, que recebe um genérico
    especificado por mim, este sendo um AudioClip (um efeito sonoro, em outras palavras).  
    Eu utilizei essa operação somente para o asset mp3 da música principal, uma vez que ela o meu asset mais pesado e 
    carregar ele de maneira normal traria um "gargalo" pro fps do jogo. Se eu aplicasse isso para todos os outros pequenos 
    assets do jogo, isso prejudicaria a experiência inicial do jogador no jogo, porque os elementos visuais "pipocariam" na
    tela ao invés de serem gerados de uma única vez. 
    */


}
