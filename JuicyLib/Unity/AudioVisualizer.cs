using UnityEngine;

namespace JuicyLib.Unity.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioVisualizer : MonoBehaviour
    {
        public AudioSource _audioSource;

        float[] _samples;
        int nSamples = 512;

        float[] _freqBand;
        int nFreqBands = 8;

        float[] bandBuffer;
        float[] _bufferDecrease;

        public void Start()
        {
            if (_audioSource == null)
            {
                Debug.Log("No audio source specified/connected!");
            }

            _samples = new float[nSamples];
            _freqBand = new float[nFreqBands];

            bandBuffer = new float[_freqBand.Length];
            _bufferDecrease = new float[bandBuffer.Length];
        }

        public void Update()
        {
            GetScpectrumAudioSource();
            MakeFrequencyBands();
            BandBuffer();
        }

        void GetScpectrumAudioSource()
        {
            _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
        }

        void BandBuffer()
        {
            for (int iFreq = 0; iFreq < _freqBand.Length; iFreq++)
            {
                if (_freqBand[iFreq] > bandBuffer[iFreq])
                {
                    bandBuffer[iFreq] = _freqBand[iFreq];
                    _bufferDecrease[iFreq] = 0.005f;
                }

                if (_freqBand[iFreq] < bandBuffer[iFreq])
                {
                    bandBuffer[iFreq] -= _bufferDecrease[iFreq];
                    _bufferDecrease[iFreq] *= 1.2f;
                }
            }
        }

        void MakeFrequencyBands()
        {
            int iSample = -1;

            for (int iFreq = 0; iFreq < nFreqBands; iFreq++)
            {
                float average = 0;
                float sampleCount = (int)(NSamples / nFreqBands);//(int)((iFreq + 1.0) / nFreqBands * NSamples);

                for (int iFreqS = 0; iFreqS < sampleCount; iFreqS++)
                {
                    iSample++;
                    if (iSample >= NSamples)
                        break;

                    average += _samples[iSample] * (iSample + 1);
                }

                average /= iSample;

                _freqBand[iFreq] = average * 10; // Multiply it by 10 so it's less close to 0
            }
        }

        public AudioSource _AudioSource
        {
            get { return _audioSource; }
            set { _audioSource = value; }
        }

        public float[] Samples
        {
            get { return _samples; }
        }

        public int NSamples
        {
            get { return nSamples; }
            set { nSamples = value; }
        }

        public float[] Frequencybands
        {
            get { return _freqBand; }
        }

        public int nFrequencybands
        {
            get { return nFreqBands; }
            set { nFreqBands = value; }
        }

        public float[] _BandBuffer
        {
            get { return bandBuffer; }
        }
    }
}
