using System;
namespace Common
{
    public interface IRemote
    {
        string Hello();
        string Modify(ref int val);
    }
}
