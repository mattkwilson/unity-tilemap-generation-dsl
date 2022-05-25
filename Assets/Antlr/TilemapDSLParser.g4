parser grammar TilemapDSLParser;
options { tokenVocab=TilemapDSLLexer; }

program : canvas (statement | function)*;
statement : (loop|if|color|noiseMap|noise|fill|call);
// 'Canvas' width height
canvas : CANVAS_START INTEGER INTEGER;
// 'Loop' (x 0 to 100 step 1)
// 'EndLoop'
loop : LOOP_START STATEMENT_DEF_START VAR INTEGER LOOP_DEF_TO INTEGER LOOP_DEF_STEP INTEGER
STATEMENT_DEF_END statement* LOOP_END;
// 'Fill:' X Y width height 'in' color
fill : FILL_START PARAM PARAM INTEGER INTEGER COLOR_IN TEXT;
// 'Color:' Name R G B
color : COLOR_START TEXT COLOR_CHANNEL COLOR_CHANNEL COLOR_CHANNEL;
// 'NoiseMap:' Name frequency scale
noiseMap : NOISEMAP_START TEXT INTEGER INTEGER;
// 'Noise:' Name x y 'from' noiseMapName
noise : NOISE_START TEXT PARAM PARAM NOISE_FROM TEXT;
// 'Function:' Name
//      statement
// 'EndFunction'
function : FUNCTION_START TEXT statement* FUNCTION_END;
// 'Call' FunctionName X Y
call : CALL TEXT INTEGER INTEGER;
// 'If' (Number Condition Number)
// 'EndIf'
if : IF_START STATEMENT_DEF_START NUMBER CONDITION NUMBER STATEMENT_DEF_END statement* IF_END;
