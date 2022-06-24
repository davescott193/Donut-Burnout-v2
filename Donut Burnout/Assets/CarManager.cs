using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [Header("Cars")]
    public GameObject CarPrefab;
    public Transform CarStartTransform;
    public Transform CarEndTransform;
    public List<CarData> CarDataList = new List<CarData>();
    public float CarTimerFloat;
    public float NewCarThresholdFloat;
    public List<Color> CarColorList = new List<Color>();
    [System.Serializable]
    public class CarData
    {
        public Transform CarTransform;
        public int Directionint = 0;
        public float CarSpeedFloat = 1;
    }
    // Update is called once per frame
    void Update()
    {
        CarTimerFloat += Time.deltaTime;

        if (CarTimerFloat > NewCarThresholdFloat)
        {
            NewCarThresholdFloat = Random.Range(1, 3);
            CarTimerFloat = 0;

            CarData carData = new CarData();
            carData.Directionint = Random.Range(0, 2);
            carData.CarSpeedFloat = Random.Range(10, 20);
            carData.CarTransform = Instantiate(CarPrefab, CarStartTransform.GetChild(carData.Directionint)).transform;
            carData.CarTransform.GetChild(1).GetComponent<MeshRenderer>().material.color = CarColorList[Random.Range(0, CarColorList.Count)];
            CarDataList.Add(carData);
        }

        for (int i = 0; i < CarDataList.Count; i++)
        {
            Transform locationTransform = CarEndTransform.GetChild(CarDataList[i].Directionint);

            CarDataList[i].CarTransform.position = Vector3.MoveTowards(CarDataList[i].CarTransform.position, locationTransform.position, CarDataList[i].CarSpeedFloat * Time.deltaTime);

            if ((int)CarDataList[i].CarTransform.position.x == (int)locationTransform.position.x)
            {
                Destroy(CarDataList[i].CarTransform.gameObject);
                CarDataList.RemoveAt(i);
            }
        }

    }
}
