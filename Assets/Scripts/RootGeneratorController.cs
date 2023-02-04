using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class RootGeneratorController : MonoBehaviour {
    public float maxLength = 20;
    public float spawnAnotherChance = 0.01f;
    public float minSegmentLength = 1;
    public float maxSegmentLength = 5;
    public float newBranchAngle = 30;
    public float anglePerSecond = 50f;
    public float growthSpeed = 5;
    public float angleSpeed = 2;
    public float maxAngle = 90;

    float segmentLen = 0;
    [HideInInspector]
    public float rootLen = 0;

    float randomAngleOffset = 0;

    // Start is called before the first frame update
    void Start() {
        randomAngleOffset = Random.Range(0, 2 * Mathf.PI);
    }

    float RandomPlusMinus(float value, float divBy = 2) {
        return Random.Range(value - value / divBy, value + value / divBy);
    }

    // Update is called once per frame
    void Update() {

        Vector3 step = transform.up * Time.deltaTime * growthSpeed;
        float distance = Vector3.Distance(step + transform.position, transform.position);
        segmentLen += distance;
        rootLen += distance;
        transform.localPosition += step;

        //if (Mathf.Abs(transform.localRotation.eulerAngles.z) <= maxAngle / 2) {
            float angleStep = Mathf.Sin(Time.time + randomAngleOffset) * Time.deltaTime * anglePerSecond;
            transform.Rotate(new Vector3(0, 0, 1), RandomPlusMinus(angleStep));
        //}
        float rand = Random.Range(0, 1f);

        if (rootLen > maxLength) {
            // end of root
            enabled = false;
        } else if (segmentLen > maxSegmentLength || (segmentLen > minSegmentLength && rand < spawnAnotherChance)) {
            // spawn another generator, reset len of this to default values
            segmentLen = 0;

            RootGeneratorController child = Instantiate(this, transform.parent);
            var randomBranchAngle = RandomPlusMinus(newBranchAngle);
            child.transform.Rotate(new Vector3(0, 0, randomBranchAngle / 2));
            transform.Rotate(new Vector3(0, 0, -randomBranchAngle / 2));

            child.GetComponent<TrailRenderer>().widthMultiplier *= Mathf.Max(0.02f, 1 - rootLen / maxLength);
            child.angleSpeed = RandomPlusMinus(child.angleSpeed, 4f);
        }
    }
}
