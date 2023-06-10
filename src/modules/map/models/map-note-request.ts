import { ApiProperty } from '@nestjs/swagger';
import {
    MaxLength,
    MinLength
} from "class-validator";

export class MapNoteRequest {

    @ApiProperty()
    mapId: number;

    @MinLength(3, { message: 'Текст заметки должен состояить не менее чем из 3 символов' })
    @MaxLength(100, { message: 'Текст заметки должен состоять не более чем из 100 символов' })
    @ApiProperty()
    text: string;
}
