using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Mehmet_mp3
{
    public partial class Form1 : Form
    {
        private IWavePlayer waveOutDevice;
        private AudioFileReader audioFileReader;
        private List<string> playList = new List<string>();
        private int currentSongIndex = -1;
        private string _selectedOutputDevice = "";
        private string selectedSongTitle;
        private string selectedArtist;
        private List<string> outputDevices = new List<string>();
        private List<string> completedSongs = new List<string>();
        private List<string> songDurations = new List<string>();
        private List<string> favoriteSongs = new List<string>();


        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }
        private void InitializeForm()
        {
            PopulateOutputDeviceList();
            waveOutDevice = new WaveOutEvent();
            AllowDragDrop();
            timer1.Interval = 500;
            timer1.Tick += timer1_Tick;
            comboBox1.SelectedIndex = 0;
           

            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            
        }
        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Burada süre metni için gerekli ölçüm yapın
            string songTitle = (string)listBox1.Items[e.Index];
            int separatorIndex = songTitle.LastIndexOf('.');
            string songName = songTitle.Substring(0, separatorIndex);

            // Şarkı adı için gerekli ölçüm yapın
            SizeF nameSize = e.Graphics.MeasureString(songName, listBox1.Font);

            // Süre metni için küçük bir alan bırakarak toplam boyutu belirleyin
            string duration = GetSongDuration(playList[e.Index]);
            SizeF durationSize = e.Graphics.MeasureString(" - " + duration, listBox1.Font);

            // Şarkı adı ve süre metni için toplam boyutu hesaplayın
            e.ItemHeight = (int)Math.Max(nameSize.Height, durationSize.Height);
        }
        private void PopulateOutputDeviceList()
        {
            try
            {
                comboBox1.Items.Clear();
                outputDevices.Clear();

                for (int i = 0; i < WaveOut.DeviceCount; i++)
                {
                    var capabilities = WaveOut.GetCapabilities(i);
                    outputDevices.Add(capabilities.ProductName);
                }

                comboBox1.Items.AddRange(outputDevices.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ses aygıtları ayarlanırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AllowDragDrop()
        {
            listBox1.AllowDrop = true;
            listBox1.DragEnter += ListBox1_DragEnter;
            listBox1.DragDrop += ListBox1_DragDrop;
        }
        private void ListBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void ListBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            AddSongsToList(files);
        }
        private void onceki_sarki_Click(object sender, EventArgs e)
        {
            if (currentSongIndex > 0)
            {
                timer1.Stop();
                listBox1.SelectedIndex = --currentSongIndex;
                oynat_Click(sender, e);
            }
        }
        private void sonraki_sarki_Click(object sender, EventArgs e)
        {
            if (currentSongIndex < playList.Count - 1)
            {
                timer1.Stop();
                listBox1.SelectedIndex = ++currentSongIndex;
                oynat_Click(sender, e);
            }
        }
        private void oynat_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                PlaySelectedSong();
            }
        }
        private void PlaySelectedSong()
        {
            try
            {
                if (waveOutDevice.PlaybackState == PlaybackState.Playing)
                {
                    waveOutDevice.Stop();
                }

                audioFileReader?.Dispose();

                string selectedSongPath = playList[listBox1.SelectedIndex];
                audioFileReader = new AudioFileReader(selectedSongPath);

                waveOutDevice.Stop();
                waveOutDevice.Dispose();

                int deviceIndex = !string.IsNullOrEmpty(_selectedOutputDevice) ? outputDevices.IndexOf(_selectedOutputDevice) : -1;
                waveOutDevice = new WaveOutEvent { DeviceNumber = deviceIndex };

                waveOutDevice.Init(audioFileReader);
                waveOutDevice.Play();

                currentSongIndex = listBox1.SelectedIndex;

                if (!completedSongs.Contains(selectedSongPath))
                {
                    completedSongs.Add(selectedSongPath);
                }

                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Şarkı çalınırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void duraklat_Click(object sender, EventArgs e)
        {
            if (waveOutDevice.PlaybackState == PlaybackState.Playing)
            {
                waveOutDevice.Pause();
            }
            else if (waveOutDevice.PlaybackState == PlaybackState.Paused)
            {
                waveOutDevice.Play();
            }
        }

        private void durdur_Click(object sender, EventArgs e)
        {
            waveOutDevice?.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (audioFileReader != null)
            {
                waveOutDevice.Volume = trackBar1.Value / 150f;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowFileDialogAndAddSongs();
            for (int i = 0; i < playList.Count; i++)
            {
                songDurations.Add(GetSongDuration(playList[i]));
            }

        }

        private void ShowFileDialogAndAddSongs()
        {
            using (var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "MP3 Files|*.mp3|WAV Files|*.wav|All Files|*.*"
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    AddSongsToList(openFileDialog.FileNames);
                }
            }
        }
        private void AddSongsToList(string[] songPaths)
        {
            foreach (var path in songPaths)
            {
                if (!playList.Contains(path))
                {
                    playList.Add(path);
                    listBox1.Items.Add(Path.GetFileName(path));

                    TagLib.File tagFile = TagLib.File.Create(path);
                    if (tagFile.Tag.Pictures.Length > 0)
                    {
                        var picture = tagFile.Tag.Pictures[0];
                        using (MemoryStream ms = new MemoryStream(picture.Data.Data))
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                }
            }
            TrackCompletedSongs();
        }
        private const string PythonPath = "C:\\Python39\\python.exe";
        private const string ScriptPath = @"C:\path\to\predict_genre.py";
        private async void Lgoritma_Click(object sender, EventArgs e)
        {
            await RunGenrePredictionAsync();
        }

        private async Task RunGenrePredictionAsync()
        {
            if (listBox1.SelectedItem == null)
            {
                ShowErrorMessage("Lütfen çalma listesinden bir şarkı seçin.");
                return;
            }

            string selectedSongPath = listBox1.SelectedItem.ToString();

            try
            {
                await PredictGenreAsync(selectedSongPath);
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Tür tahmininde hata oluştu:\n{ex.Message}:\npyton yok bilgisayarinizda");
            }
        }

        private async Task PredictGenreAsync(string songPath)
        {
            var processStartInfo = CreateProcessStartInfo(songPath);
            var process = new Process { StartInfo = processStartInfo };

            await RunProcessAsync(process);
        }

        private ProcessStartInfo CreateProcessStartInfo(string songPath)
        {
            return new ProcessStartInfo
            {
                FileName = PythonPath,
                Arguments = $"\"{ScriptPath}\" \"{songPath}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };
        }

        private async Task RunProcessAsync(Process process)
        {
            process.Start();
            string output = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(output))
            {
                ShowGenrePrediction(output.Trim());
            }
            else
            {
                throw new Exception("Python boş çıktı döndürdü.");
            }
        }

        private void ShowGenrePrediction(string genre)
        {
            MessageBox.Show($"Tahmin Edilen Tür: {genre}");
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message);
        }


        private List<Image> randomImages = new List<Image>();

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (audioFileReader != null)
            {
                int trackBarValue = trackBar2.Value;
                double newPositionInSeconds = (trackBarValue / 100.0) * audioFileReader.TotalTime.TotalSeconds;
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(newPositionInSeconds);
            }
        }
        private void TrackCompletedSongs()
        {
            for (int i = listBox1.Items.Count - 1; i >= 0; i--)
            {
                string fileName = listBox1.Items[i].ToString();
                if (completedSongs.Contains(fileName))
                {
                    listBox1.Items.RemoveAt(i);
                    listBox1.Items.Insert(0, fileName);
                }
            }
        }
        private void delete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                int selectedIndex = listBox1.SelectedIndex;
                playList.RemoveAt(selectedIndex);
                listBox1.Items.RemoveAt(selectedIndex);

                if (currentSongIndex == selectedIndex)
                {
                    waveOutDevice.Stop();
                    audioFileReader.Dispose();
                    audioFileReader = null;
                    currentSongIndex = -1;
                }
                else if (currentSongIndex > selectedIndex)
                {
                    currentSongIndex--;
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (audioFileReader != null)
            {
                UpdateTrackBar();
                CheckSongCompletion();
            }
        }
        private void UpdateTrackBar()
        {
            double currentTime = audioFileReader.CurrentTime.TotalSeconds;
            double totalTime = audioFileReader.TotalTime.TotalSeconds;

            int trackBarPosition = (int)((currentTime / totalTime) * 100);

            if (trackBar2.InvokeRequired)
            {
                trackBar2.Invoke(new Action(() => trackBar2.Value = trackBarPosition));
            }
            else
            {
                trackBar2.Value = trackBarPosition;
            }
        }
        private void CheckSongCompletion()
        {
            if (audioFileReader.CurrentTime.TotalSeconds >= audioFileReader.TotalTime.TotalSeconds)
            {
                TrackCompletedSongs();

                if (currentSongIndex < playList.Count - 1)
                {
                    currentSongIndex++;
                    listBox1.SelectedIndex = currentSongIndex;
                    oynat_Click(null, null);
                }
                else
                {
                    waveOutDevice.Stop();
                    timer1.Stop();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedOutputDevice = (string)comboBox1.SelectedItem;
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0 && e.Index < listBox1.Items.Count)
            {
                string songName = listBox1.Items[e.Index].ToString();
                string duration = songDurations[e.Index]; // Süreyi önceden hesaplanmış listeden alıyoruz

                Brush brush = Brushes.Black;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    brush = Brushes.White;
                    e.Graphics.FillRectangle(Brushes.DarkBlue, e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.LightGray, e.Bounds);
                }

                // Şarkı adı ve süre metni çizdirme
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(songName, e.Font, brush, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 50, e.Bounds.Height), stringFormat);

                // Süre metnini sağa hizalayarak çizdirme
                stringFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(duration, e.Font, brush, new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 10, e.Bounds.Height), stringFormat);
            }
        }
        private string GetSongDuration(string songPath)
        {
            using (var reader = new AudioFileReader(songPath))
            {
                return reader.TotalTime.ToString(@"mm\:ss"); // Şarkı süresini dakika ve saniye cinsinden formatlayın
            }
        }

        private void file_nameedit_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedSong = listBox1.SelectedItem.ToString();
                var (dialogResult, newName) = ShowRenameSongDialog(selectedSong);

                // Güncellenmiş ad ile dosyanın yolunu oluşturun
                if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(newName))
                {
                    int selectedIndex = listBox1.SelectedIndex;
                    string oldPath = playList[selectedIndex];
                    string newPath = Path.Combine(Path.GetDirectoryName(oldPath), $"{newName}{Path.GetExtension(oldPath)}");

                    // Dosyanın yolunu güncelle ve taglarını da yeniden oku
                    File.Move(oldPath, newPath);
                    audioFileReader?.Dispose();
                    audioFileReader = new AudioFileReader(newPath);

                    playList[selectedIndex] = newPath;
                    listBox1.Items[selectedIndex] = Path.GetFileName(newPath); // Listbox'ın gösterdiği adı güncelle

                    if (currentSongIndex == selectedIndex)
                    {
                        waveOutDevice.Stop();
                        waveOutDevice.Init(audioFileReader);
                        waveOutDevice.Play();
                    }

                    TrackCompletedSongs(); // Eksik şarkıların listesini güncel tut
                }
            }
        }
        private (DialogResult, string) ShowRenameSongDialog(string currentName)
        {
            var renameForm = new Form
            {
                Text = "Şarkı Adını Değiştir",
                StartPosition = FormStartPosition.CenterScreen,
                Width = 300,
                Height = 100
            };

            Label label = new Label
            {
                Text = "Yeni Şarkı Adı:",
                Location = new Point(10, 20),
                AutoSize = true
            };

            TextBox textBox = new TextBox
            {
                Location = new Point(110, 18),
                Text = Path.GetFileNameWithoutExtension(currentName) // Başlangıç değerini mevcut adla başlat
            };

            Button saveButton = new Button
            {
                Text = "Kaydet",
                Dock = DockStyle.Bottom,
                DialogResult = DialogResult.OK
            };
            Button cancelButton = new Button
            {
                Text = "İptal",
                Dock = DockStyle.Bottom,
                DialogResult = DialogResult.Cancel // İptal butonunun dialog sonucu olarak ayarlanması
            };

            saveButton.Click += (sender, args) => renameForm.DialogResult = DialogResult.OK;
            cancelButton.Click += (sender, args) => renameForm.DialogResult = DialogResult.Cancel;

            renameForm.Controls.Add(label);
            renameForm.Controls.Add(textBox);
            renameForm.Controls.Add(saveButton);
            renameForm.Controls.Add(cancelButton);

            var result = renameForm.ShowDialog(); // Formu göster ve kullanıcıdan giriş bekler
            return (result, textBox.Text);
        }
    }
}
