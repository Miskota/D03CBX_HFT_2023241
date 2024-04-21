using D03CBX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace D03CBX_GUI_2023242.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Record> Records { get; set; }
        public RestCollection<Album> Albums { get; set; }
        public RestCollection<Writer> Writers { get; set; }

        public ICommand CreateRecordCommand { get; set; }
        public ICommand DeleteRecordCommand { get; set; }
        public ICommand UpdateRecordCommand { get; set; }

        public ICommand CreateAlbumCommand { get; set; }
        public ICommand DeleteAlbumCommand { get; set; }
        public ICommand UpdateAlbumCommand { get; set; }

        public ICommand CreateWriterCommand { get; set; }
        public ICommand DeleteWriterCommand { get; set; }
        public ICommand UpdateWriterCommand { get; set; }



        private Record selectedRecord;
        private Album selectedAlbum;
        private Writer selectedWriter;
        public  Record SelectedRecord {
            get { return selectedRecord; }
            set {
                if (value != null) {
                    selectedRecord = new Record() { 
                        RecordID = value.RecordID,
                        Title = value.Title,
                        Plays = value.Plays,
                        Genre = value.Genre,
                        Rating = value.Rating,
                        Duration = value.Duration
                    };

                    //SetProperty(ref selectedRecord, value);
                    OnPropertyChanged();
                    (DeleteRecordCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Album SelectedAlbum {
            get { return selectedAlbum; }
            set {
                if (value != null) {
                    selectedAlbum = new Album() {
                        AlbumID = value.AlbumID,
                        AlbumName = value.AlbumName,
                        ReleaseYear = value.ReleaseYear,
                        Genre = value.Genre
                    };
                    //SetProperty(ref selectedAlbum, value);
                    OnPropertyChanged();
                    (DeleteAlbumCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public Writer SelectedWriter {
            get { return selectedWriter; }
            set {
                if (value != null) {
                    selectedWriter = new Writer() {
                        WriterName = value.WriterName,
                        WriterID = value.WriterID,
                        YearOfBirth = value.YearOfBirth
                    };
                    //SetProperty(ref selectedWriter, value);
                    OnPropertyChanged();
                    (DeleteWriterCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public static bool IsInDesignMode {
            get {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
        public MainWindowViewModel()
        {
            

            if (!IsInDesignMode) {
                Records = new RestCollection<Record>("http://localhost:59244/", "record");
                Albums = new RestCollection<Album>("http://localhost:59244/", "album");
                Writers = new RestCollection<Writer>("http://localhost:59244/", "writer");

                CreateRecordCommand = new RelayCommand(() => {
                    Records.Add(new Record() {
                        // ID not required, handled on server side
                        Title = SelectedRecord.Title,
                        Plays = SelectedRecord.Plays,
                        Genre = SelectedRecord.Genre,
                        Rating = SelectedRecord.Rating,
                        Duration = SelectedRecord.Duration
                    });
                });

                DeleteRecordCommand = new RelayCommand(() => {
                    Records.Delete(SelectedRecord.RecordID);
                },
                () => {
                    return SelectedRecord != null;
                });

                UpdateRecordCommand = new RelayCommand(() => {
                    Records.Update(SelectedRecord);
                });





                CreateAlbumCommand = new RelayCommand(() => {
                    Albums.Add(new Album() {
                        AlbumName = SelectedAlbum.AlbumName,
                        Genre = SelectedAlbum.Genre,
                        ReleaseYear = SelectedAlbum.ReleaseYear
                    });
                });

                DeleteAlbumCommand = new RelayCommand(() => {
                    Albums.Delete(SelectedAlbum.AlbumID);
                },
                () => {
                    return SelectedAlbum != null;
                });

                UpdateAlbumCommand = new RelayCommand(() => {
                    Albums.Update(SelectedAlbum);
                });





                CreateWriterCommand = new RelayCommand(() => {
                    Writers.Add(new Writer() {
                        WriterName = SelectedWriter.WriterName,
                        YearOfBirth = SelectedWriter.YearOfBirth,
                    });
                });

                DeleteWriterCommand = new RelayCommand(() => {
                    Writers.Delete(SelectedWriter.WriterID);
                },
                () => {
                    return SelectedWriter != null;
                });

                UpdateWriterCommand = new RelayCommand(() => {
                    Writers.Update(SelectedWriter);
                });


                // If the selected item is null it throws an exception if you try to create a new item
                // Solution
                SelectedRecord = new Record();
                SelectedAlbum = new Album();
                SelectedWriter = new Writer();
            }
        }
    }
}
