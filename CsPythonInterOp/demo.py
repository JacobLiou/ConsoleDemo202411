# python 调用C#
import clr
clr.AddReference(r'D:\Project Demo\Demo\Console\CsPythonInterOp\bin\Debug\net9.0\CsPythonInterOp.dll')

from CsPythonInterOp import CalcService

# 调用 C# 方法
result = CalcService.Add(5.0, 3.0)
print(result)



# C# 调用 Python 