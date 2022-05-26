lexer grammar TilemapDSLLexer;

CANVAS_START: 'Canvas';

LOOP_START: 'Loop';
LOOP_END: 'EndLoop';
LOOP_DEF_TO: 'to';
LOOP_DEF_STEP: 'step';

IF_START: 'If' WS* -> mode(TEXT_MODE);
IF_END: 'EndIf';

FILL_START: 'Fill';
COLOR_START: 'Color:' WS* -> mode(TEXT_MODE);
COLOR_IN: 'in' WS* -> mode(TEXT_MODE);
NOISEMAP_START: 'NoiseMap:' WS* -> mode(TEXT_MODE);
NOISE_START: 'Noise:' WS* -> mode(TEXT_MODE);
NOISE_FROM: 'from' WS* -> mode(TEXT_MODE);

FUNCTION_START: 'Function:' WS* -> mode(TEXT_MODE);
FUNCTION_END: 'EndFunction';
CALL: 'Call' WS* -> mode(TEXT_MODE);

INTEGER: ([1-9][0-9]*|'0');
VAR: ('x'|'y');

CONDITION: (('<'|'>')'='?|'=='|'!=');

WS: [\r\n\t ] -> channel(HIDDEN);

mode TEXT_MODE;
TEXT: [a-zA-Z]+ -> mode(DEFAULT_MODE);