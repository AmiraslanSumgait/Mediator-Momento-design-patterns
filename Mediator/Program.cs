using System;

namespace Mediator
{
    public interface IMotherboard
    {
        void Change(Unit birim);
    }
    public abstract class Unit
    {
        protected IMotherboard _motherboard;
        public Unit(IMotherboard motherboard)
        {
            _motherboard = motherboard;
        }
    }
    public class CDDriver : Unit
    {
        public CDDriver(IMotherboard motherboard) : base(motherboard)
        {
        }
        string _cdData;
        public string CDData => _cdData;
        public void UseCd()
        {
            _cdData = "görüntü1,görüntü2,görüntü3*ses1,ses2,ses3";
            _motherboard.Change(this);
        }
    }
    public class CPU : Unit
    {
        public CPU(IMotherboard motherboard) : base(motherboard)
        {
        }
        string _videoData, _sounddata;
        public string VideoData => _videoData;
        public string SoundData => _sounddata;
        public void UseData(string cdData)
        {
            string[] array = cdData.Split("*");
            _videoData = array[0]; //görüntü değerleri video data olarak alınıyor.
            _sounddata = array[1]; //ses değerleri ses data olarak alınıyor.
            _motherboard.Change(this);
        }
    }
    public class GraphicCard : Unit
    {
        public GraphicCard(IMotherboard motherboard) : base(motherboard)
        {
        }
        public void DisplayData(string videoData)
        {
            string[] datas = videoData.Split(",");
            foreach (string data in datas)
                Console.WriteLine($"Display : {data}");
        }
    }
    public class SoundCard : Unit
    {
        public SoundCard(IMotherboard motherboard) : base(motherboard)
        {
        }
        public void VolumeAdd(string soundData)
        {
            string[] datas = soundData.Split(",");
            foreach (string data in datas)
                Console.WriteLine($"Incoming sound : {data}");
        }
    }
    public class MotherBoard : IMotherboard
    {
        CDDriver _cdDriver;
        CPU _cpu;
        GraphicCard _graphicCard;
        SoundCard _soundCard;
        public CDDriver CDDriver { set => _cdDriver = value; }
        public CPU CPU { set => _cpu = value; }
        public GraphicCard GraphicCard { set => _graphicCard = value; }
        public SoundCard SoundCard { set => _soundCard = value; }
        public void Change(Unit unit)
        {
            if (unit is CDDriver)
            {
                string cdData = _cdDriver.CDData;
                _cpu.UseData(cdData);
            }
            else if (unit is CPU)
            {

                string videoData = _cpu.VideoData;
                string soundData = _cpu.SoundData;
                _graphicCard.DisplayData(videoData);
                _soundCard.VolumeAdd(soundData);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MotherBoard motherBoard = new MotherBoard();
            CDDriver cdDriver = new CDDriver(motherBoard);
            CPU cpu = new CPU(motherBoard);
            GraphicCard graphicCard = new GraphicCard(motherBoard);
            SoundCard soundCard = new SoundCard(motherBoard);
            motherBoard.CDDriver = cdDriver;
            motherBoard.CPU = cpu;
            motherBoard.GraphicCard = graphicCard;
            motherBoard.SoundCard = soundCard;
            cdDriver.UseCd();
        }
    }
}