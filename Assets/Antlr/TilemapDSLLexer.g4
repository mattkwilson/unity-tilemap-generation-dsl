lexer grammar TilemapDSLLexer;

CANVAS_START: 'Canvas';

LOOP_START: 'Loop';
LOOP_END: 'LoopEnd';
LOOP_DEF_TO: 'to';
LOOP_DEF_FROM: 'from';
LOOP_DEF_STEP: 'step';

STATEMENT_DEF_START: '(';
STATEMENT_DEF_END: ')';

FILL_START: 'Fill';
COLOR_START: 'Color:';
NOISEMAP_START: 'NoiseMap:';
NOISE_START: 'Noise:';

TEXT_HACK: '_' -> mode(TEXT_MODE);

PARAM: (VAR|INTEGER);
VAR: ('x'|'y');
COLOR_CHANNEL: [0-9]+; //TODO: input proper regex (0-255)
INTEGER: [0-9]+; // TODO: handle leading 0s
WS: [\r\n\t ] -> channel(HIDDEN);

mode TEXT_MODE;
TEXT: [a-zA-Z]+ -> mode(DEFAULT_MODE);