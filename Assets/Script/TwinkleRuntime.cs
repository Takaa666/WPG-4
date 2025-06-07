using UnityEngine;

public class TwinkleRuntime : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    public float frequency = 2f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[1];
    }

    void Update()
    {
        int count = ps.GetParticles(particles);
        if (count > 0)
        {
            float alpha = Mathf.Abs(Mathf.Sin(Time.time * frequency));

            Color currentColor = particles[0].startColor;
            currentColor.a = alpha;
            particles[0].startColor = currentColor;

            ps.SetParticles(particles, count);
        }
    }
}
