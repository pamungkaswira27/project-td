using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class GameOverAnimation : MonoBehaviour
    {
        public float durasiAnimasi = 2f; // Durasi animasi dalam detik
        public float faktorZoom = 1.5f; // Faktor zoom-in

        void Start()
        {
            // Memulai Coroutine untuk animasi zoom in pada saat objek aktif
            StartCoroutine(ZoomIn());
        }

        IEnumerator ZoomIn()
        {
            Vector3 skalaAwal = transform.localScale;
            Vector3 skalaAkhir = skalaAwal * faktorZoom;
            float waktuMulai = Time.time;

            while (Time.time - waktuMulai < durasiAnimasi)
            {
                float kemajuan = (Time.time - waktuMulai) / durasiAnimasi;
                transform.localScale = Vector3.Lerp(skalaAwal, skalaAkhir, kemajuan);
                yield return null; // Menunggu frame selanjutnya
            }

            // Pastikan skala objek setelah animasi selesai
            transform.localScale = skalaAkhir;
        }
    }
}