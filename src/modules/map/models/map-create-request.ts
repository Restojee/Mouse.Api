import { ApiProperty } from '@nestjs/swagger';
import {
  MaxLength,
  MinLength
} from "class-validator";

export class MapCreateRequest {

  @MinLength(3, { message: 'Название карты должно состоять не менее чем из 3 символов' })
  @MaxLength(20, { message: 'Название карты должно состоять не более чем из 50 символов' })
  @ApiProperty()
  name: string;

  @MinLength(3, { message: 'Текст комментария должен состоять не менее чем из 3 символов' })
  @MaxLength(50, { message: 'Описание карты должно состоять не более чем из 50 символов' })
  @ApiProperty()
  description?: string;
}
