lexer grammar TilemapDSLLexer;

CANVAS_START: 'Canvas';

LOOP_START: 'Loop';
LOOP_END: 'EndLoop';
LOOP_DEF_TO: 'to';
LOOP_DEF_STEP: 'step';

IF_START: 'If' WS* -> mode(TEXT_MODE);
IF_END: 'EndIf';

FILL_START: 'Fill';
TEXTURE_START: 'Texture:' WS* -> mode(TEXT_MODE);
COLOR_START: 'Color:' WS* -> mode(TEXT_MODE);
FILL_IN: 'in' WS* -> mode(TEXT_MODE);
NOISEMAP_START: 'NoiseMap:' WS* -> mode(TEXT_MODE);
NOISE_START: 'Noise:' WS* -> mode(TEXT_MODE);
NOISE_FROM: 'from' WS* -> mode(TEXT_MODE);

FUNCTION_START: 'Function:' WS* -> mode(TEXT_MODE);
FUNCTION_END: 'EndFunction';
CALL: 'Call' WS* -> mode(TEXT_MODE);
FUNCTION_PARAM_START: '(' WS* -> mode(TEXT_MODE);
FUNCTION_PARAM_SEP: ',' WS* -> mode(TEXT_MODE);
FUNCTION_PARAM_END: ')';

INTEGER: ([1-9][0-9]*|'0');
VAR: ('x'|'y');

CONDITION: (('<'|'>')'='?|'=='|'!=');

WS: [\r\n\t ] -> channel(HIDDEN);

// TODO: Need to restrict variable names, can't use 'x' 'y' (reserved for loops)

mode TEXT_MODE;
TEXT: [a-zA-Z0-9]+ -> mode(DEFAULT_MODE);
