
from eip import PLC


def ex_read(tag):
    ret = comm.Read(tag)
    return ret


def ex_write(tag, value):
    comm.Write(tag, value)


def write_tag(tag, value):
    ex_write(tag, value)


def set_comn_tag(ip, slot):
    comm.IPAddress = ip
    comm.ProcessorSlot = int(slot)


comm = PLC()

