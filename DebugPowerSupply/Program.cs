using System;
using System.IO.Ports;
using Modbus;
using NModbus;
using NModbus.Serial;

namespace DebugPowerSupply
{
    internal class Program
    {
        private static IModbusFactory modbusFactory;
        private static IModbusSerialMaster ModbusController;

        private static SerialPortAdapter serialPortAdapter;
        private static SerialPort serialPort;

        static void Main(string[] args)
        {
            serialPort = new SerialPort("COM4");
            serialPort.Open();
            modbusFactory = new ModbusFactory();
            serialPortAdapter = new SerialPortAdapter(serialPort);
            ModbusController = modbusFactory.CreateRtuMaster(serialPortAdapter);


        }

        public static void  SendCommand(IModbusSerialMaster modbusController)
        {
            modbusController.WriteSingleRegister(1, 0x10, 1); //напряжение 
            modbusController.WriteSingleRegister(1, 0x11, 0x4);
        }


        public static void ReadParams(IModbusSerialMaster modbusController)
        {
            var result = modbusController.ReadInputRegisters(1, 0, 1);

            foreach(var r in result)
            {
                Console.Write($"{r} ");
            }
            Console.WriteLine();
        }

    }
}
