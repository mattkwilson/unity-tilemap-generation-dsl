using System.Collections;
using System.Collections.Generic;
using Antlr4.Runtime;
using Assets.Scripts.AST;
using UnityEngine;

using UnityEditor;
using Color = UnityEngine.Color;

[CustomEditor(typeof(TilemapGenerator))]
public class TilemapGeneratorEditor : Editor
{
    private TilemapGenerator tilemapGenerator;
    private Vector2 scrollPosition = Vector2.zero;
    void OnEnable() {
        tilemapGenerator = target as TilemapGenerator;
    }

    void OnDisable() {
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        if(GUILayout.Button("Random Seed")) {
            int seed = Random.Range(-9999999, 9999999);
            serializedObject.FindProperty("Seed").intValue = seed;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(500));
        serializedObject.FindProperty("DSLInput").stringValue = EditorGUILayout.TextArea(serializedObject.FindProperty("DSLInput").stringValue, GUILayout.ExpandHeight(true));

        EditorGUILayout.EndScrollView();
        if(GUILayout.Button("Generate Tilemap")) {
            string input = serializedObject.FindProperty("DSLInput").stringValue;
            TilemapDSLLexer lexer = new TilemapDSLLexer(new CodePointCharStream(input));
            lexer.Reset();
            ITokenStream tokens = new CommonTokenStream(lexer);
            TilemapDSLParser parser = new TilemapDSLParser(tokens);
            ParseTreeToAST visitor = new ParseTreeToAST();
            ASTBase program = visitor.VisitProgram(parser.program());
            program.Accept(tilemapGenerator, new Evaluator());
        }
        serializedObject.ApplyModifiedProperties();
        tilemapGenerator.UpdateTransparentTextureList();
    }
}
