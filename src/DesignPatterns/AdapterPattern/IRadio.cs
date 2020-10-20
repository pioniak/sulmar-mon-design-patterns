using System;
using System.Collections.Generic;
using System.Text;

namespace AdapterPattern
{
    // Adapter
    // wariant klasowy
    public class HyteraRadioAdapter2 : HyteraRadio, IRadio
    {
        public HyteraRadioAdapter2()
            : base()
        {
        }

        public void Send(string message, byte channel)
        {
            base.Init();

            base.SendMessage(channel, message);

            base.Release();
        }
    }

    public class MotorolaRadioAdapter2 : MotorolaRadio, IRadio
    {
        private readonly string pincode;

        public MotorolaRadioAdapter2(string pincode)
            : base()
        {
            this.pincode = pincode;
        }

        public void Send(string message, byte channel)
        {
            base.PowerOn(pincode);

            base.SelectChannel(channel);

            base.Send(message);

            base.PowerOff();
        }
    }

    // Adapter
    // wariant obiektowy
    public interface IRadio
    {
        void Send(string message, byte channel);
    }

    public class HyteraRadioAdapter : IRadio
    {
        // Adeptee
        private HyteraRadio radio;

        public HyteraRadioAdapter()
        {
            radio = new HyteraRadio();
        }

        public void Send(string message, byte channel)
        {
            radio.Init();

            radio.SendMessage(channel, message);

            radio.Release();
        }
    }

    public class MotorolaRadioAdapter : IRadio
    {
        private MotorolaRadio radio;

        private readonly string pincode;

        public MotorolaRadioAdapter(string pincode)
        {
            this.pincode = pincode;
        }

        public void Send(string message, byte channel)
        {
            radio.PowerOn(pincode);

            radio.SelectChannel(channel);

            radio.Send(message);

            radio.PowerOff();
        }
    }
}
