using D03CBX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
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
                SetProperty(ref selectedRecord, value);
                (DeleteRecordCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public Album SelectedAlbum {
            get { return selectedAlbum; }
            set { 
                SetProperty(ref selectedAlbum, value);
                (DeleteAlbumCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public Writer SelectedWriter {
            get { return selectedWriter; }
            set { 
                SetProperty(ref selectedWriter, value);
                (DeleteWriterCommand as RelayCommand).NotifyCanExecuteChanged();
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

                    });
                });

                DeleteRecordCommand = new RelayCommand(() => {
                    Records.Delete(SelectedRecord.RecordID);
                },
                () => {
                    return SelectedRecord != null;
                });

                UpdateRecordCommand = new RelayCommand(() => {

                });





                CreateAlbumCommand = new RelayCommand(() => {

                });

                DeleteAlbumCommand = new RelayCommand(() => {
                    Albums.Delete(SelectedAlbum.AlbumID);
                },
                () => {
                    return SelectedAlbum != null;
                });

                UpdateAlbumCommand = new RelayCommand(() => {

                });





                CreateWriterCommand = new RelayCommand(() => {

                });

                DeleteWriterCommand = new RelayCommand(() => {
                    Writers.Delete(SelectedWriter.WriterID);
                },
                () => {
                    return SelectedWriter != null;
                });

                UpdateWriterCommand = new RelayCommand(() => {

                });
            }
        }
    }
}
