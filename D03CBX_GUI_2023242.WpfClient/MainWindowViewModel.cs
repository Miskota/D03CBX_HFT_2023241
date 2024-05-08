using D03CBX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
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
        //public RestCollection<Writer> Top10AlbumCount { get; set; }


        public ICommand CreateRecordCommand { get; set; }
        public ICommand DeleteRecordCommand { get; set; }
        public ICommand UpdateRecordCommand { get; set; }

        public ICommand CreateAlbumCommand { get; set; }
        public ICommand DeleteAlbumCommand { get; set; }
        public ICommand UpdateAlbumCommand { get; set; }

        public ICommand CreateWriterCommand { get; set; }
        public ICommand DeleteWriterCommand { get; set; }
        public ICommand UpdateWriterCommand { get; set; }

        // Non-Crud Commands
        public ICommand Top10AlbumCountCommand { get; set; } // IEnumerable<Writer>
        public ICommand GenreStatisticsCommand { get; set; } // IEnumerable<string>
        public ICommand Top10RatedCommand { get; set; } // IEnumerable<Record>
        public ICommand Top10PlaysCommand { get; set; } // IEnumerable<Record>
        public ICommand AveragePlaysAlbumCommand { get; set; } // IEnumerable<string>



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


        // For non-crud
        private RestService nonCrudRestService;

        private IEnumerable<Writer> top10AlbumCount;
        public IEnumerable<Writer> Top10AlbumCount {
            get { return top10AlbumCount; }
            set { SetProperty(ref top10AlbumCount, value);}
        }

        private IEnumerable<Record> top10Rated;
        public IEnumerable<Record> Top10Rated {
            get { return top10Rated; }
            set { SetProperty(ref top10Rated, value); }
        }

        public MainWindowViewModel()
        {
            

            if (!IsInDesignMode) {
                nonCrudRestService = new("http://localhost:59244/");
                Records = new RestCollection<Record>("http://localhost:59244/", "record", "hub");
                Albums = new RestCollection<Album>("http://localhost:59244/", "album", "hub");
                Writers = new RestCollection<Writer>("http://localhost:59244/", "writer", "hub");
                

                Top10AlbumCountCommand = new RelayCommand(() =>
                {
                    Top10AlbumCount = nonCrudRestService.Get<Writer>("NonCrud/Top10AlbumCount");
                });

                GenreStatisticsCommand = new RelayCommand(() =>
                {
                    
                });

                Top10RatedCommand = new RelayCommand(() =>
                {
                    Top10Rated = nonCrudRestService.Get<Record>("NonCrud/Top10Rated");
                });

                Top10PlaysCommand = new RelayCommand(() =>
                {
                   
                });

                AveragePlaysAlbumCommand = new RelayCommand(() =>
                {
                    
                });


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
