using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class RootGeneratorController : MonoBehaviour
{
    public int maxLength = 20;
    public float spawnAnotherChance = 0.01f;
    public int minSegmentLength = 1;
    public int maxSegmentLength = 5;
    public int newBranchAngle = 30;
    public float anglePerSecond = 50f;
    public float growthSpeed = 5;
    public float angleSpeed = 2;
    public float maxAngle = 90;

    public float angle = 0;
    public float segmentLen = 0;
    public float rootLen = 0;
    public float rootAngle = 0;


    float randomAngleOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        randomAngleOffset = Random.Range(0, 2 * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 step = transform.up * Time.deltaTime * growthSpeed;
        float distance = Vector3.Distance(step+transform.position, transform.position);
        segmentLen += distance;
        rootLen += distance;
        transform.localPosition += step;

        angle = Mathf.Sin(Time.time + randomAngleOffset) * anglePerSecond;
        //angle += Mathf.Sin(Random.Range(0,2*Mathf.PI)) / 10;
        //angle = Mathf.Min(angle, maxAngle);
        //angle = Mathf.Max(angle, -maxAngle);

        if(Mathf.Abs(transform.localRotation.eulerAngles.z) <= maxAngle/2)
        {
            float angleStep = angle * Time.deltaTime * angleSpeed;
            rootAngle += angleStep;
            transform.Rotate(new Vector3(0, 0, 1), angleStep);
        }
        float rand = Random.Range(0, 1f);

        if(rootLen > maxLength)
        {
            // end of root
            enabled = false;
        }
        else if(segmentLen > maxSegmentLength || (segmentLen > minSegmentLength && rand < spawnAnotherChance))
        {
            // spawn another generator, reset len of this to default values
            segmentLen = 0;

            RootGeneratorController child = Instantiate(this,transform.parent);
            child.transform.Rotate(new Vector3(0,0,newBranchAngle/2));
            transform.Rotate(new Vector3(0, 0, -newBranchAngle/2));

            child.GetComponent<TrailRenderer>().widthMultiplier *= Mathf.Max(0.02f,1 - rootLen / maxLength);
            child.angleSpeed = Random.Range(child.angleSpeed - child.angleSpeed / 4, child.angleSpeed + child.angleSpeed / 4);
        }
    }
}
