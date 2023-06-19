import {ApiProperty} from "@nestjs/swagger";
import {MaxLength, MinLength} from "class-validator";

export class CommentCreateRequest {

    @ApiProperty()
    mapId: number;

    @MinLength(3, { message: 'Текст комментария должен состояить не менее чем из 3 символов' })
    @MaxLength(100, { message: 'Текст комментария должен состоять не более чем из 100 символов' })
    @ApiProperty()
    text: string
}