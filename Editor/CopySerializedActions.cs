using UnityEditor;

public static class CopySerializedActions
{
    static SerializedObject _operationSource;

    private static void Copy(MenuCommand command)
    {
        _operationSource = new SerializedObject(command.context);
    }

    private static void Paste(MenuCommand command)
    {
        SerializedObject dest = new SerializedObject(command.context);
        
        SerializedProperty propertyIterator = _operationSource.GetIterator();
        
        if (propertyIterator.NextVisible(true))
        {
            while (propertyIterator.NextVisible(true))
            {
                SerializedProperty property = dest.FindProperty(propertyIterator.name); 

                if (property != null && property.propertyType == propertyIterator.propertyType) 
                {
                    dest.CopyFromSerializedProperty(propertyIterator); 
                }
            }
        }
        
        dest.ApplyModifiedProperties();
        
        EditorUtility.SetDirty(dest.targetObject);
    }
    

    [MenuItem("CONTEXT/Component/Copy Serialized Values")]
    public static void CopySerializedFromBase(MenuCommand command)
    {
        Copy(command);
    }

    [MenuItem("CONTEXT/Component/Paste Serialized Values")]
    public static void PasteSerializedFromBase(MenuCommand command)
    {
        Paste(command);
    }
    
    [MenuItem("CONTEXT/ScriptableObject/Copy Serialized Values")]
    public static void CopyObjectSerializedFromBase(MenuCommand command)
    {
        Copy(command);
    }

    [MenuItem("CONTEXT/ScriptableObject/Paste Serialized Values")]
    public static void PasteObjectSerializedFromBase(MenuCommand command)
    {
        Paste(command);
    }
}