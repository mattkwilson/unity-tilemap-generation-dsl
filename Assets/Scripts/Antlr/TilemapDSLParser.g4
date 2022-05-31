parser grammar TilemapDSLParser;
options { tokenVocab=TilemapDSLLexer; }

program : function* canvas statement*;
statement : (loop|if|fill|call|variable);
variable : (color|noiseMap|noise|texture);
// 'Canvas' width height
canvas : CANVAS_START INTEGER INTEGER;
// 'Loop' (x 0 to 100 step 1)
// 'EndLoop'
loop : LOOP_START VAR INTEGER LOOP_DEF_TO INTEGER LOOP_DEF_STEP INTEGER statement* LOOP_END;
// 'Fill:' X Y width height 'in' color
fill : FILL_START (INTEGER|VAR) (INTEGER|VAR) INTEGER INTEGER FILL_IN TEXT;
// 'Color:' Name R G B
color : COLOR_START TEXT INTEGER INTEGER INTEGER;
// 'Texture:' Name Index
texture : TEXTURE_START TEXT INTEGER;
// 'NoiseMap:' Name frequency scale
noiseMap : NOISEMAP_START TEXT INTEGER INTEGER;
// 'Noise:' Name x y 'from' noiseMapName
noise : NOISE_START TEXT (INTEGER|VAR) (INTEGER|VAR) NOISE_FROM TEXT;
// 'Function:' Name
//      statement
// 'EndFunction'
function : FUNCTION_START TEXT (FUNCTION_PARAM_START TEXT (FUNCTION_PARAM_SEP TEXT)* FUNCTION_PARAM_END)? statement* FUNCTION_END;
// 'Call' FunctionName X Y
call : CALL TEXT (INTEGER|VAR) (INTEGER|VAR) (FUNCTION_PARAM_START TEXT (FUNCTION_PARAM_SEP TEXT)* FUNCTION_PARAM_END)?;
// 'If' (Number Condition Number)
// 'EndIf'
if : IF_START TEXT CONDITION INTEGER statement* IF_END;


