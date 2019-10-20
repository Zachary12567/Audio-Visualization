using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizer : MonoBehaviour {
    public AudioSource music;
    public static float[] samples = new float[sampleAmmt];

    public float[] freqBand = new float[bandAmmt];
    public static float[] bandBuffer = new float[sampleAmmt];
    public float[] bufferDecrease = new float[sampleAmmt];

    public float bufferDStartingVal;
    public float bufferDMultiplier;

    private const int sampleAmmt = 256;
    private const int bandAmmt = 8;

    //public static AudioVisualizer Instance;

    void Start () {
        music = GetComponent<AudioSource>();
	}
    void Update () {
        GetSpectrumData();
        BandBuffer();
        BandTogether();
	}
    private void GetSpectrumData() {
        music.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
    private void BandBuffer() {
        for(int i = 0; i < sampleAmmt; i++) {
            if (samples[i] > bandBuffer[i]) {
                bandBuffer[i] = samples[i];
                bufferDecrease[i] = bufferDStartingVal;
            }
            if (samples[i] < bandBuffer[i]) {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= bufferDMultiplier;
            }
        }
    }
    private void BandTogether() {
        for (int i = 0; i < sampleAmmt; i++) {
            freqBand[(i / bandAmmt)] += samples[i];
        }
    }
}
