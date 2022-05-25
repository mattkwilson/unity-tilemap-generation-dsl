lexer grammar TilemapDSLLexer;

CANVAS_START: 'Canvas';

LOOP_START: 'Loop';
LOOP_END: 'EndLoop';
LOOP_DEF_TO: 'to';
LOOP_DEF_STEP: 'step';

STATEMENT_DEF_START: '(';
STATEMENT_DEF_END: ')';

IF_START: 'If';
IF_END: 'EndIf';

FILL_START: 'Fill';
COLOR_START: 'Color:';
COLOR_IN: 'in' WS* -> mode(TEXT_MODE);
NOISEMAP_START: 'NoiseMap:';
NOISE_START: 'Noise:';
NOISE_FROM: 'from' WS* -> mode(TEXT_MODE);

FUNCTION_START: 'Function:';
FUNCTION_END: 'EndFunction';
CALL: 'Call' WS* -> mode(TEXT_MODE);

PARAM: (VAR|INTEGER);
VAR: ('x'|'y');
COLOR_CHANNEL: [2]([0-4][0-9]|[5][0-5])|[1][0-9][0-9]|[1-9][0-9]|[0-9];
INTEGER: [1-9][0-9]*;
CONDITION: (('<'|'>')'='?|'=='|'!=');

WS: [\r\n\t ] -> channel(HIDDEN);

mode TEXT_MODE;
TEXT: [a-zA-Z]+ -> mode(DEFAULT_MODE);