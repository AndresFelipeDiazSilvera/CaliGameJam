using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject enemy2;
    [SerializeField] List<GameObject> enemies = new List<GameObject>();
    [SerializeField] int rangeSpawnMax=10;
    [SerializeField] int rangeSpawnMin=-10;
    public int poolSize = 10;
    public int wave;
    public bool isSpawningWave;
    public int spawnRate;
    public int enemiesPerWave = 2;
    public int enemiesSpawnedInWave = 0;
    public int blockSize = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeEnemyPool();
        StartCoroutine(SpawnWaveController());
    }

    // Update is called once per frame
    void Update()
    {

    }



    //metodo para inicializar el pool
    private void InitializeEnemyPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            // creamos un gameObject para inicializar los enemigos
            GameObject newEnemy;
            if (i % 2 == 0) // Alterna entre enemy y enemy2
            {
                newEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity, transform); // Se establece el SpawnManager como padre
            }
            else
            {
                newEnemy = Instantiate(enemy2, Vector3.zero, Quaternion.identity, transform);
            }
            newEnemy.SetActive(false); // Los desactivamos inicialmente
            enemies.Add(newEnemy); // Los añadimos al pool
        }
        Debug.Log($"Pool de enemigos inicializado con {enemies.Count} enemigos.");
    }

    //metodo para obtener el enemigo del pool
    public GameObject GetEnemyFromPool()
    {
        foreach (var Enemy in enemies)
        {
            if (!Enemy.activeInHierarchy)
            {
                Enemy.transform.position = SpawnPoint();
                Enemy.SetActive(true);
                return Enemy;
            }
        }
        //si no hay enemigos disponibles crea uno nuevo y lo devolvemos 
        GameObject newEnemy;
        newEnemy = Instantiate(enemy, SpawnPoint(), Quaternion.identity);
        newEnemy.SetActive(true);
        enemies.Add(newEnemy);
        return newEnemy;
    }

    //metodo para devolver un enemigo al pool
    public void ReturnEnemyToPool(GameObject enemyToReturn)
    {
        if (enemyToReturn != null)
        {
            enemyToReturn.SetActive(false);
            enemyToReturn.transform.position = Vector3.zero;
        }
    }
    //metodo para el punto de spawn
    public Vector2 SpawnPoint()
    {
        return new Vector2(Random.Range(rangeSpawnMin, rangeSpawnMax), Random.Range(rangeSpawnMin, rangeSpawnMax));
    }



    // Corrutina principal que controla todo el flujo de las oleadas
    public IEnumerator SpawnWaveController()
    {
        yield return new WaitForSeconds(1f); // Pequeña espera inicial antes de la primera oleada

        while (true) // Este bucle permite que el juego continue con nuevas oleadas indefinidamente
        {
            //iniciar una nueva oleada
            if (!isSpawningWave)
            {
                wave++; // Incrementa el numero de oleada
                blockSize += 2;
                // Ajusta la cantidad de enemigos para la nueva oleada
                if (wave == 1)
                {
                    enemiesPerWave = 2;
                }
                else
                {
                    enemiesPerWave += 2;
                }

                enemiesSpawnedInWave = 0; // Reinicia el contador de enemigos spaneados para la nueva oleada
                isSpawningWave = true;
                Debug.Log($"--- Iniciando Oleada {wave}. Total enemigos para esta oleada: {enemiesPerWave} ---");
            }

            // espera para spawnear el siguiente bloque o si la oleada ya termino (todos spawnearon y murieron).
            while (EnemysEnable() > 0 || enemiesSpawnedInWave >= enemiesPerWave)
            {
                // Si todos los enemigos de la oleada han sido spaneados Y derrotados, la oleada ha terminado.
                if (enemiesSpawnedInWave >= enemiesPerWave && EnemysEnable() == 0)
                {
                    isSpawningWave = false; // Marca que la oleada actual ha finalizado
                    Debug.Log($"--- Oleada {wave} completada. ---");
                    break; // Salimos de este bucle interno para que el bucle while(true) pueda iniciar la siguiente oleada.
                }
                yield return null; // Espera un frame antes de re-verificar las condiciones
            }

            // Si la oleada ya termino (por la condicion de 'break' anterior), saltamos a la siguiente iteracion del bucle principal
            if (!isSpawningWave)
            {
                yield return new WaitForSeconds(5f); // Pausa entre oleadas
                continue; // Vuelve al inicio del while(true) para empezar la proxima oleada
            }

            //Spawnea el siguiente bloque de enemigos
            int remainingEnemiesInWave = enemiesPerWave - enemiesSpawnedInWave;
            int numEnemiesInThisBlock = Mathf.Min(blockSize, remainingEnemiesInWave);

            if (numEnemiesInThisBlock > 0)
            {
                Debug.Log($"Spawneando bloque de {numEnemiesInThisBlock} enemigos.");
                for (int i = 0; i < numEnemiesInThisBlock; i++)
                {
                    GetEnemyFromPool(); // Obtiene y activa un enemigo del pool
                    enemiesSpawnedInWave++; // Incrementa el contador de enemigos spaneados
                    yield return new WaitForSeconds(spawnRate); // Espera entre la aparicion de cada enemigo
                }
            }
            else
            {
                Debug.LogWarning("No hay enemigos para spawnear en este bloque, pero la oleada aun no ha terminado.");
                yield return null; // Evita un bucle infinito
            }
        }
    }

    //metodos para contar los enemigos activos
    public int EnemysEnable()
    {
        int count = 0;
        foreach (GameObject prefab in enemies)
        {
            if (prefab.activeInHierarchy)
            {
                count++;
            }
        }
        return count;

    }
    //metodo para manegar la muerte de los enemigos 
    public void EnemyDied(GameObject enemyDie)
    {
        enemyDie.SetActive(false);
    }
}
