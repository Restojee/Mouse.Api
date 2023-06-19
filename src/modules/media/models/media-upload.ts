import { ApiProperty } from '@nestjs/swagger';

export class MediaUpload {
  @ApiProperty({ type: 'string', format: 'binary' })
  file: Express.Multer.File;
}
