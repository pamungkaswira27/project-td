using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace ProjectTD
{
    public class CreditAnimation : MonoBehaviour
    {
        public Image creditImage;
        public float scrollDistance = 2000f; // Jarak yang ingin di-scroll

        void Start()
        {
            // Pastikan creditImage tidak null
            if (creditImage != null)
            {
                // Atur posisi awal creditImage
                creditImage.rectTransform.localPosition = new Vector3(0f, -scrollDistance, 0f);

                // Membuat animasi credit
                creditImage.rectTransform.DOLocalMoveY(0f, 10f) // Pindahkan gambar dari bawah ke atas dalam 10 detik
                    .SetEase(Ease.Linear) // Pilih jenis animasi (Anda dapat mengganti dengan Ease lainnya)
                    .OnComplete(OnAnimationComplete) // Panggil fungsi ketika animasi selesai
                    .SetLoops(-1, LoopType.Incremental) // Set loop ke nilai -1 agar animasi berjalan terus menerus dan LoopType.Incremental agar kembali ke awal setelah mencapai akhir
                    .SetAutoKill(false); // Matikan opsi AutoKill agar animasi tidak otomatis dihentikan
            }
            else
            {
                Debug.LogError("Gambar Credit tidak ditetapkan. Harap tentukan objek gambar pada Inspector.");
            }
        }

        void OnAnimationComplete()
        {
            creditImage.rectTransform.DOKill(); // Hentikan animasi sebelum memulai kembali
            creditImage.rectTransform.localPosition = new Vector3(0f, -scrollDistance, 0f); // Reset posisi ke bawah
            creditImage.rectTransform.DOLocalMoveY(0f, 10f).SetEase(Ease.Linear).OnComplete(OnAnimationComplete).SetLoops(-1, LoopType.Incremental).SetAutoKill(false);
        }
    }
}