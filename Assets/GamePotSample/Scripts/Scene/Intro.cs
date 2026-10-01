using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePotDemo
{
    public class Intro : MonoBehaviour
    {
        [SerializeField]
        private float introInterval;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine("IntroSequence");
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator IntroSequence()
        {
            yield return new WaitForSeconds(introInterval);
            SceneManager.LoadSceneAsync("Login");
        }
    }
}


