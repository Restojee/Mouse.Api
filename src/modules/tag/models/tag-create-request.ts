import { ApiProperty } from "@nestjs/swagger";
import { MaxLength, MinLength } from "class-validator";

export class TagCreateRequest {

    @MinLength(3, { message: 'Текст комментария должен состояить не менее чем из 3 символов' })
    @MaxLength(20, { message: 'Текст комментария должен состоять не более чем из 20 символов' })
    @ApiProperty()
    name: string

    @MinLength(3, { message: 'Текст комментария должен состояить не менее чем из 3 символов' })
    @MaxLength(100, { message: 'Текст комментария должен состоять не более чем из 100 символов' })
    @ApiProperty()
    description?: string
}